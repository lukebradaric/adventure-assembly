using AdventureAssembly.Core;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class Enemy : Unit
    {
        new public EnemyStats Stats => (EnemyStats)base.Stats;
        public EnemyData EnemyData { get; private set; }

        public override void Initialize(UnitData unitData, Vector2Int position)
        {
            EnemyData = (EnemyData)unitData;

            base.Initialize(unitData, position);
        }

        public override void Die()
        {
            base.Die();

            ExperienceManager.Instance.AddExperience(EnemyData.KillExperience);

            Destroy(gameObject);
        }
    }
}