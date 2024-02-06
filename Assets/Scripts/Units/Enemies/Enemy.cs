using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class Enemy : CharacterUnit
    {
        [Space]
        [Header("Events")]
        [SerializeField] private EnemyScriptableEvent _enemyDamagedScriptableEvent;

        new public EnemyStats Stats => (EnemyStats)base.Stats;
        public EnemyData EnemyData { get; private set; }

        private EnemyNavigation _navigation;

        public override void Initialize(CharacterUnitData unitData, Vector2Int position)
        {
            EnemyData = (EnemyData)unitData;
            _navigation = EnemyData.Navigation.GetClone();

            base.Initialize(unitData, position);
        }

        /// <summary>
        /// Makes this enemy attack the target unit.
        /// </summary>
        /// <param name="targetUnit">The unit to attack</param>
        public void Attack(CharacterUnit targetUnit)
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

        public void OnNavigate()
        {
            _navigation.Update(this);
        }

        protected override void OnDie()
        {
            base.OnDie();

            Debug.Log(this.Stats.GoldDropChance.Value);
            // Enemy chance to spawn gold on death
            if (this.Stats.GoldDropChance.Value > Random.value)
            {
                GoldManager.Instance.AddGold(Position);
            }

            // Enemy add experience on death
            ExperienceManager.Instance.AddExperience(EnemyData.KillExperience);
        }

        protected override void OnTakeDamage(DamageData damageData)
        {
            base.OnTakeDamage(damageData);
            _enemyDamagedScriptableEvent?.Invoke(this);
        }
    }
}