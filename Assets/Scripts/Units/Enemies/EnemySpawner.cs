using AdventureAssembly.Core;
using System.Collections.Generic;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    [System.Serializable]
    internal struct EnemySpawnData
    {
        public EnemyData EnemyData;
        public int Weight;
    }

    public class EnemySpawner : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Enemy _enemyPrefab;

        [Space]
        [Header("Settings")]
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private Vector2 _spawnRadius;
        [SerializeField] private AnimationCurve _spawnCurve;
        [SerializeField] private List<EnemySpawnData> _spawnData;

        private float _currentTime = 0f;

        private void OnEnable()
        {
            TickManager.EnemyTickUpdate += OnEnemyTickUpdate;
        }

        private void OnDisable()
        {
            TickManager.EnemyTickUpdate -= OnEnemyTickUpdate;
        }

        private void OnEnemyTickUpdate()
        {
            _currentTime += TickManager.Instance.TickInterval;

            SpawnRandomEnemies(GetEnemySpawnCount());
        }

        private int GetEnemySpawnCount()
        {
            return (int)Mathf.Ceil(_spawnCurve.Evaluate(_currentTime));
        }

        private List<EnemyData> GetRandomEnemyData(int count)
        {
            WeightedList<EnemyData> weightedEnemyData = new WeightedList<EnemyData>();
            foreach (EnemySpawnData spawnData in _spawnData)
            {
                weightedEnemyData.Add(spawnData.EnemyData, spawnData.Weight);
            }

            List<EnemyData> enemyData = new List<EnemyData>();
            for (int i = 0; i < count; i++)
            {
                enemyData.Add(weightedEnemyData.Random());
            }

            return enemyData;
        }

        private void SpawnRandomEnemies(int count)
        {
            List<EnemyData> enemyData = GetRandomEnemyData(count);

            foreach (EnemyData enemy in enemyData)
            {
                SpawnEnemy(enemy);
            }
        }

        private Vector2Int GetSpawnPosition()
        {
            Vector2 center = transform.position;

            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            if (randomDirection == Vector2.zero) randomDirection = Vector2.right;

            float radius = Random.Range(_spawnRadius.x, _spawnRadius.y);

            Vector2 spawnPosition = center + (randomDirection * radius);

            return new Vector2Int((int)spawnPosition.x, (int)spawnPosition.y);
        }

        private void SpawnEnemy(EnemyData enemyData)
        {
            if (EnemyManager.Units.Count >= _maxEnemyCount)
            {
                Debug.LogWarning("Reach max enemy count. Skipping spawn.");
                return;
            }

            Vector2Int spawnPosition = GetSpawnPosition();

            Enemy enemy = Instantiate(_enemyPrefab, (Vector2)spawnPosition, Quaternion.identity);

            EnemyManager.Instance.AddUnit(enemy);

            enemy.Initialize(enemyData, spawnPosition);
            enemy.Died += OnEnemyDied;
        }

        private void OnEnemyDied(Unit unit)
        {
            unit.Died -= OnEnemyDied;
            EnemyManager.Instance.RemoveUnit((Enemy)unit);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius.x);
            Gizmos.DrawWireSphere(transform.position, _spawnRadius.y);
        }
    }
}