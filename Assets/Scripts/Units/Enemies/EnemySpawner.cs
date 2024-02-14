using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemySpawner : SerializedMonoBehaviour
    {
        [BoxGroup("Components")]
        [OdinSerialize] public EnemySpawnData EnemySpawnData { get; protected set; }

        [BoxGroup("Debugging")]
        [OdinSerialize] public float CurrentTime { get; protected set; } = 0;

        private Coroutine _spawnCoroutine = null;

        private void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private void OnDisable()
        {
            StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(EnemySpawnData.SpawnInterval);
            CurrentTime += EnemySpawnData.SpawnInterval;
            TrySpawnEnemies();
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private void TrySpawnEnemies()
        {
            // Calculate the amount of enemies to spawn
            int spawnCount = EnemySpawnData.GetEnemySpawnCount(CurrentTime) - EnemyManager.Instance.Units.Count;
            if (spawnCount <= 0)
            {
                return;
            }

            // Spawn enemies
            List<EnemyData> enemiesToSpawn = EnemySpawnData.GetEnemiesToSpawn(CurrentTime, spawnCount);
            foreach (EnemyData enemyData in enemiesToSpawn)
            {
                enemyData.Create(LevelMap.Instance.GetRandomPositionWithinEnemyMap());
            }
        }
    }
}