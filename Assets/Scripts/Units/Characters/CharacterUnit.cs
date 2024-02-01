using AdventureAssembly.Core;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    [SelectionBase]
    public abstract class CharacterUnit : Unit
    {
        [PropertySpace]
        [Title("Components")]
        [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }
        [OdinSerialize] public virtual CharacterUnitStats Stats { get; private set; }

        public CharacterUnitData CharacterUnitData { get; private set; }

        public int CurrentHealth { get; protected set; }
        public bool IsDead { get; protected set; } = false;

        /// <summary>
        /// The last DamageData that this unit took. Null by default.
        /// </summary>
        public DamageData LastDamageTaken { get; protected set; } = null;

        public Vector2Int LastPosition { get; protected set; }

        public event Action<DamageData> Damaged;
        public event Action<CharacterUnit> Died;

        public List<CharacterUnitModifier> Modifiers { get; protected set; } = new List<CharacterUnitModifier>();

        /// <summary>
        /// Initializes this unit with data and a position.
        /// </summary>
        /// <param name="unitData">The UnitData for this unit to use.</param>
        /// <param name="position">The starting position of this unit</param>
        public virtual void Initialize(CharacterUnitData unitData, Vector2Int position)
        {
            base.Initialize(position);

            this.CharacterUnitData = unitData;
            this.Stats.Initialize(this);
            this.CurrentHealth = Stats.GetMaxHealth();

            foreach (CharacterUnitModifier modifier in CharacterUnitData.Modifiers)
            {
                CharacterUnitModifier newModifier = modifier.GetClone();
                newModifier.Apply(this);
                Modifiers.Add(newModifier);
            }

            SpriteRenderer.sprite = CharacterUnitData.Sprite;
            name = $"{CharacterUnitData.Name}";
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

            // Flip sprite to face movement
            this.FlipSprite(direction.x);

            CharacterUnitData.MovementTween.Animate(this, Position, TickManager.Instance.TickInterval);
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

            // Assign this as the Unit that is taking damage
            damageData.TargetCharacterUnit = this;

            // Subtract from health
            CurrentHealth -= damageData.Value;

            // Store damageData in last damage taken
            LastDamageTaken = damageData;

            // Invoke event and call OnMethod
            Damaged?.Invoke(damageData);
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
            OnDie();
            Destroy();
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
        /// Called when this unit dies. Called before destroying.
        /// </summary>
        protected virtual void OnDie() { }

        /// <summary>
        /// Called before this unit is destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
            // Clear all modifiers
            foreach (CharacterUnitModifier modifier in Modifiers)
            {
                modifier.Remove(this);
            }

            Modifiers.Clear();
        }
    }
}