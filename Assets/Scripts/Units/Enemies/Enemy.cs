using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class Enemy : Unit
    {
        [OdinSerialize] public EnemyData EnemyData { get; private set; }

        private void Start()
        {
            // Debug
            Initialize(EnemyData, new Vector2Int((int)transform.position.x, (int)transform.position.y));
        }

        public override void Initialize(UnitData unitData, Vector2Int position)
        {
            base.Initialize(unitData, position);

            EnemyData = (EnemyData)unitData;
            EnemyManager.Instance.AddUnit(this);
        }

        public override void Die()
        {
            base.Die();

            EnemyManager.Instance.RemoveUnit(this);
            Destroy(gameObject);
        }
    }
}