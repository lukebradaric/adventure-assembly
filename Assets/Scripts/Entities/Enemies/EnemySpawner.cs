using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnPattern _enemySpawnPattern = null;

    private void OnEnable()
    {
        TurnManager.TurnUpdate += OnTurnUpdate;
    }

    private void OnDisable()
    {
        TurnManager.TurnUpdate -= OnTurnUpdate;
    }

    private void OnTurnUpdate()
    {
        List<Enemy> enemyPrefabs = _enemySpawnPattern.GetEnemiesToSpawn(TurnManager.CurrentTurn);

        foreach (Enemy enemy in enemyPrefabs)
        {
            // If enemies reached max spawn count
            if (EnemyManager.Entities.Count >= _enemySpawnPattern.maxConcurrentEnemies)
            {
                return;
            }

            Instantiate(enemy, _enemySpawnPattern.GetEnemySpawnPosition(), Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(CharacterManager.GetCenter(), Camera.main.orthographicSize + _enemySpawnPattern.spawnRadiusBuffer);
    }
}
