using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;

namespace AdventureAssembly.Units
{
    [SelectionBase]
    public abstract class Unit : SerializedMonoBehaviour
    {
        [PropertySpace]
        [Title("Components")]
        [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }
        [OdinSerialize] public virtual UnitStats Stats { get; private set; }

        public UnitData UnitData { get; private set; }

        public int CurrentHealth { get; protected set; }

        public Vector2Int Position { get; protected set; }
        public Vector2Int LastPosition { get; protected set; }

        public bool IsDead { get; protected set; } = false;

        public event Action<Unit> Died;

        public virtual void Initialize(UnitData unitData, Vector2Int position)
        {
            this.UnitData = unitData;
            this.Position = position;
            this.Stats.Initialize(this);
            this.CurrentHealth = Stats.GetMaxHealth();

            SpriteRenderer.sprite = UnitData.Sprite;
            name = $"{UnitData.Name}";
        }

        public virtual void OnTick() { }

        public virtual void Move(Vector2Int direction)
        {
            // Save last position
            LastPosition = Position;

            // Update new position
            Position += direction;

            // Flip sprite to face movement
            this.FlipSprite(direction.x);

            UnitData.MovementTween.Animate(this, Position, TickManager.Instance.TickInterval);
        }

        public virtual void Damage(DamageData damageData)
        {
            if (IsDead)
            {
                return;
            }

            CurrentHealth -= damageData.Value;

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            IsDead = true;
            Died?.Invoke(this);
        }

        public virtual void FlipSprite(int x)
        {
            if (x == 0)
            {
                return;
            }

            Vector3 localScale = transform.localScale;

            if (x > 0)
                localScale.x = Mathf.Abs(localScale.x);
            else if (x < 0)
                localScale.x = -Mathf.Abs(localScale.x);

            transform.localScale = localScale;
        }
    }
}