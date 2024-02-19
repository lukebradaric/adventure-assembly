using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemySpawner : SerializedMonoBehaviour
    {
        [BoxGroup("Components")]
        [OdinSerialize] public EnemySpawnData EnemySpawnData { get; protected set; }

        private void OnEnable()
        {
            TimeManager.CurrentTimeUpdated += OnCurrentTimeUpdate;
        }

        private void OnDisable()
        {
            TimeManager.CurrentTimeUpdated -= OnCurrentTimeUpdate;
        }

        private void OnCurrentTimeUpdate(int currentTime)
        {
            TrySpawnEnemies(currentTime);
        }

        private void TrySpawnEnemies(int currentTime)
        {
            // Calculate the amount of enemies to spawn
            int spawnCount = EnemySpawnData.GetEnemySpawnCount(currentTime) - EnemyManager.Instance.Units.Count;
            if (spawnCount <= 0)
            {
                return;
            }

            // Spawn enemies
            List<EnemyData> enemiesToSpawn = EnemySpawnData.GetEnemiesToSpawn(currentTime, spawnCount);
            foreach (EnemyData enemyData in enemiesToSpawn)
            {
                enemyData.Create(LevelMap.Instance.GetRandomPositionWithinEnemyMap());
            }
        }
    }
}