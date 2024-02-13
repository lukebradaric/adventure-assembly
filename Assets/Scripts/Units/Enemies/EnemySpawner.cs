using System.Collections;
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

    [System.Serializable]
    internal struct EnemySpawnAmountData
    {
        public float Time;
        public int Count;
    }

    public class EnemySpawner : MonoBehaviour
    {
        private const float MaxFindSpawnPositionAttempts = 5;

        [Space]
        [Header("Components")]
        [SerializeField] private Enemy _enemyPrefab;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _updateInterval;
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private Vector2 _spawnRadius;
        [SerializeField] private AnimationCurve _spawnCurve;
        [SerializeField] private List<EnemySpawnData> _spawnData;
        [SerializeField] private List<EnemySpawnAmountData> _amountData;

        [Space]
        [Header("Debugging")]
        [SerializeField] private EnemySpawnAmountData _lastSpawnData;

        private float _currentTime = 0f;
        private Coroutine _updateCoroutine = null;

        private void OnEnable()
        {
            _updateCoroutine = StartCoroutine(UpdateCoroutine());
        }

        private IEnumerator UpdateCoroutine()
        {
            yield return new WaitForSeconds(_updateInterval);
            _currentTime += _updateInterval;
            OnUpdate();

            _updateCoroutine = StartCoroutine(UpdateCoroutine());
        }

        private void OnUpdate()
        {
            SpawnRandomEnemies(GetEnemySpawnCount());
        }

        private int GetEnemySpawnCount()
        {
            //return (int)Mathf.Ceil(_spawnCurve.Evaluate(_currentTime));
            int count = 0;
            for (int i = _amountData.Count - 1; i >= 0; i--)
            {
                if (_currentTime >= _amountData[i].Time)
                {
                    count = _amountData[i].Count;
                    _lastSpawnData = _amountData[i];
                    break;
                }
            }

            return Mathf.Max(count - EnemyManager.Instance.Units.Count, 0);
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
            Vector2Int GetRandomSpawnPosition()
            {
                Vector2 center = transform.position;

                Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                if (randomDirection == Vector2.zero) randomDirection = Vector2.right;

                float radius = Random.Range(_spawnRadius.x, _spawnRadius.y);

                Vector2 position = center + (randomDirection * radius);

                return new Vector2Int((int)position.x, (int)position.y);
            }

            Vector2Int spawnPosition = GetRandomSpawnPosition();

            // If spawn position is already occupied, try to find new position
            int attempts = 0;
            while (GridManager.HasUnitAtPosition(spawnPosition) && attempts < MaxFindSpawnPositionAttempts)
            {
                spawnPosition = GetRandomSpawnPosition();
                attempts++;
            }

            // TODO: Return null or something instead of the possibility of this being an already occupied position
            return spawnPosition;
        }

        private void SpawnEnemy(EnemyData enemyData)
        {
            if (EnemyManager.Instance.Units.Count >= _maxEnemyCount)
            {
                Debug.LogWarning("Reach max enemy count. Skipping spawn.");
                return;
            }

            Vector2Int spawnPosition = GetSpawnPosition();

            Enemy enemy = Instantiate(_enemyPrefab, (Vector2)spawnPosition, Quaternion.identity);

            enemy.Initialize(enemyData, spawnPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius.x);
            Gizmos.DrawWireSphere(transform.position, _spawnRadius.y);
        }
    }
}