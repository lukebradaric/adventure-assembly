using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class TestEnemyModifier : SerializedMonoBehaviour
    {
        public EnemyData enemyData;
        [OdinSerialize] private List<UnitModifier> modifiers = new List<UnitModifier>();
        [OdinSerialize] private List<Enemy> enemies = new List<Enemy>();

        private void Awake()
        {
            foreach (UnitModifier modifier in modifiers)
            {
                EnemyManager.AddModifier(modifier);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Initialize(enemyData, new Vector2Int((int)enemy.transform.position.x, (int)enemy.transform.position.y));
            }
        }
    }
}