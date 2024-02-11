using AdventureAssembly.Core;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    [SelectionBase]
    public abstract class Character : Unit
    {
        [PropertySpace]
        [Title("Events")]
        [OdinSerialize] private GameScriptableEvent _onCharacterDamaged;
        [OdinSerialize] private GameScriptableEvent _onCharacterHealed;

        [PropertySpace]
        [Title("Components")]
        [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }
        [OdinSerialize] public SpriteRenderer ShadowSpriteRenderer { get; private set; }
        [OdinSerialize] public virtual CharacterStats Stats { get; private set; }
        [OdinSerialize] public virtual CharacterStatusEffects StatusEffects { get; private set; }

        public CharacterData CharacterData { get; private set; }

        public int CurrentHealth { get; protected set; }
        public bool IsDead { get; protected set; } = false;

        /// <summary>
        /// The last DamageData that this unit took. Null by default.
        /// </summary>
        public DamageData LastDamageTaken { get; protected set; } = null;

        /// <summary>
        /// The last HealData that this unit took. Null by default.
        /// </summary>
        public HealData LastHealTaken { get; protected set; } = null;

        public Vector2Int LastPosition { get; protected set; }

        public event Action<DamageData> Damaged;
        public event Action<HealData> Healed;
        public event Action<Character> Died;

        public List<CharacterModifier> Modifiers { get; protected set; } = new List<CharacterModifier>();

        /// <summary>
        /// Initializes this unit with data and a position.
        /// </summary>
        /// <param name="unitData">The UnitData for this unit to use.</param>
        /// <param name="position">The starting position of this unit</param>
        public virtual void Initialize(CharacterData unitData, Vector2Int position)
        {
            base.Initialize(position);

            this.CharacterData = unitData;
            this.Stats.Initialize(this);
            this.CurrentHealth = Stats.GetMaxHealth();

            foreach (CharacterModifier modifier in CharacterData.Modifiers)
            {
                CharacterModifier newModifier = modifier.GetClone();
                newModifier.Apply(this);
                Modifiers.Add(newModifier);
            }

            SpriteRenderer.sprite = CharacterData.Sprite;
            ShadowSpriteRenderer.sprite = CharacterData.ShadowSprite;
            ShadowSpriteRenderer.transform.localPosition = CharacterData.ShadowOffset;
            name = $"{CharacterData.Name}";
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

            CharacterData.MovementTween.Animate(this, Position, TickManager.Instance.TickInterval);
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

            // Subtract from health
            CurrentHealth -= damageData.Value;

            // Store damageData in last damage taken
            LastDamageTaken = damageData;

            // Invoke event and call OnMethod
            Damaged?.Invoke(damageData);
            OnTakeDamage(damageData);
            _onCharacterDamaged?.Invoke(this, damageData);

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Heals this unit. HealData calculated beforehand.
        /// </summary>
        /// <param name="healData">The HealData to heal this unit</param>
        public virtual void Heal(HealData healData)
        {
            if (IsDead)
            {
                return;
            }

            CurrentHealth += healData.Value;

            LastHealTaken = healData;

            Healed?.Invoke(healData);
            OnHealed(healData);
            _onCharacterHealed?.Invoke(this, healData);

            if (CurrentHealth > CharacterData.MaxHealth)
            {
                CurrentHealth = CharacterData.MaxHealth;
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
        protected virtual void OnTakeDamage(DamageData damageData)
        {
            CharacterData.HurtSound?.Play();
        }

        /// <summary>
        /// Called when this unit is healed.
        /// </summary>
        /// <param name="healData">The HealData that this unit took.</param>
        protected virtual void OnHealed(HealData healData)
        {

        }

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
            foreach (CharacterModifier modifier in Modifiers)
            {
                modifier.Remove(this);
            }

            Modifiers.Clear();
        }
    }
}