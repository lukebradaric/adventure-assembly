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
        public bool IsDead { get; protected set; } = false;

        /// <summary>
        /// The last DamageData that this unit took. Null by default.
        /// </summary>
        public DamageData LastDamageTaken { get; protected set; } = null;

        public Vector2Int Position { get; protected set; } = Vector2Int.zero;
        public Vector2Int LastPosition { get; protected set; }

        public event Action<Unit> Died;

        /// <summary>
        /// Initializes this unit with data and a position.
        /// </summary>
        /// <param name="unitData">The UnitData for this unit to use.</param>
        /// <param name="position">The starting position of this unit</param>
        public virtual void Initialize(UnitData unitData, Vector2Int position)
        {
            this.UnitData = unitData;
            this.Position = position;
            this.Stats.Initialize(this);
            this.CurrentHealth = Stats.GetMaxHealth();

            GridManager.AddPosition(this.Position, gameObject);

            SpriteRenderer.sprite = UnitData.Sprite;
            name = $"{UnitData.Name}";
        }

        /// <summary>
        /// Moves this unit in a direction.
        /// </summary>
        /// <param name="direction">The direction to move this unit.</param>
        public virtual void Move(Vector2Int direction)
        {
            // Save last position
            LastPosition = Position;

            // Update new position
            Position += direction;

            GridManager.UpdatePosition(LastPosition, Position, gameObject);

            // Flip sprite to face movement
            this.FlipSprite(direction.x);

            UnitData.MovementTween.Animate(this, Position, TickManager.Instance.TickInterval);
        }

        /// <summary>
        /// Causes this unit to take damage. DamageData must be calculate beforehand.
        /// </summary>
        /// <param name="damageData">The DamageData to deal to this unit.</param>
        public virtual void TakeDamage(DamageData damageData)
        {
            if (IsDead)
            {
                return;
            }

            CurrentHealth -= damageData.Value;
            LastDamageTaken = damageData;
            OnTakeDamage(damageData);

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Kills this unit.
        /// </summary>
        public virtual void Die()
        {
            IsDead = true;
            Died?.Invoke(this);
            GridManager.RemovePosition(this.Position, gameObject);
            OnDie();
        }

        /// <summary>
        /// Flips this units sprite to a specific direction.
        /// </summary>
        /// <param name="x">The direction to flip. x > 0 = right, x < 0 = left</param>
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

        /// <summary>
        /// Called on the unit update interval.
        /// </summary>
        /// <param name="time">The time between updates.</param>
        public virtual void OnUpdate(float time) { }

        /// <summary>
        /// Called when this unit takes damage. Called after damage is applied.
        /// </summary>
        /// <param name="damageData">The DamageData that this unit took.</param>
        protected virtual void OnTakeDamage(DamageData damageData) { }

        /// <summary>
        /// Called when this unit dies. Called after death event is invoked.
        /// </summary>
        protected virtual void OnDie() { }
    }
}