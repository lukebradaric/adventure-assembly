using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class Enemy : Character
    {
        [Space]
        [Header("Events")]
        [SerializeField] private GameScriptableEvent _onEnemyDamaged;

        new public EnemyStats Stats => (EnemyStats)base.Stats;
        public EnemyData EnemyData { get; private set; }

        private EnemyNavigation _navigation;

        public override void Initialize(CharacterData unitData, Vector2Int position)
        {
            EnemyManager.Instance.AddUnit(this);
            base.Initialize(unitData, position);

            EnemyData = (EnemyData)unitData;
            _navigation = EnemyData.Navigation?.GetClone();
        }

        /// <summary>
        /// Makes this enemy attack the target unit.
        /// </summary>
        /// <param name="targetUnit">The unit to attack</param>
        public void Attack(Character targetUnit)
        {
            if (IsDead)
            {
                Debug.Log("Attacking while dead!");
            }

            // Calculate and deal damage to the target unit
            DamageData damageData = new DamageData(this, targetUnit, EnemyData.BaseDamage);
            targetUnit.TakeDamage(damageData);

            // Play the attack tween animation
            EnemyData.AttackTween.Animate(this, targetUnit.Position, TickManager.Instance.TickInterval / 2f);
        }

        public virtual void OnNavigate()
        {
            // If this enemy is stunned, do not navigate
            if (StatusEffects.Contains(StatusEffect.Stunned))
            {
                return;
            }

            _navigation?.Update(this);
        }

        protected override void OnDie()
        {
            base.OnDie();

            EnemyManager.Instance.RemoveUnit(this);

            // Enemy chance to spawn gold on death
            if (this.Stats.GetGoldDropChance() > Random.value)
            {
                GoldManager.Instance.AddGold(Position);
            }

            // Enemy add experience on death
            ExperienceManager.Instance.AddExperience(EnemyData.KillExperience);
        }

        protected override void OnTakeDamage(DamageData damageData)
        {
            base.OnTakeDamage(damageData);
            _onEnemyDamaged?.Invoke(this, damageData);
        }
    }
}