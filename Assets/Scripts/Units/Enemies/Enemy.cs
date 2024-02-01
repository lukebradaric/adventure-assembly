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

        public void OnNavigate()
        {
            _navigation.Update(this);
        }

        protected override void OnDie()
        {
            base.OnDie();
            ExperienceManager.Instance.AddExperience(EnemyData.KillExperience);
        }

        protected override void OnTakeDamage(DamageData damageData)
        {
            base.OnTakeDamage(damageData);
            _enemyDamagedScriptableEvent?.Invoke(this);
        }
    }
}