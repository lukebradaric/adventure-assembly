using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemySpawnPattern")]
public class EnemySpawnPattern : ScriptableObject
{
    public int maxConcurrentEnemies;
    public float spawnRadiusBuffer;
    //public Vector2 spawnRadiusRange;

    public AnimationCurve spawnCurve;
    public List<EnemySpawnData> enemySpawnData = new List<EnemySpawnData>();

    // Get how many enemies to spawn on (turn)
    public int GetEnemySpawnCount(int turn)
    {
        return (int)Mathf.Ceil(spawnCurve.Evaluate(turn));
    }

    public List<Enemy> GetEnemiesToSpawn(int turn)
    {
        WeightedList<Enemy> validEnemies = new WeightedList<Enemy>();

        foreach (EnemySpawnData enemyData in enemySpawnData)
        {
            if (turn >= enemyData.minSpawnTurn && turn <= enemyData.maxSpawnTurn)
            {
                validEnemies.Add(enemyData.enemyPrefab, enemyData.weight);
            }
        }

        if (validEnemies.Count <= 0)
        {
            Debug.LogError($"Could not find enemy to spawn at turn: {turn}");
            return null;
        }

        List<Enemy> enemies = new List<Enemy>();

        for (int i = 0; i < GetEnemySpawnCount(turn); i++)
        {
            enemies.Add(validEnemies.GetRandom());
        }

        return enemies;
    }

    public Vector2 GetEnemySpawnPosition()
    {
        Vector2 center = CharacterManager.GetCenter();
        Bounds bounds = CharacterManager.GetBounds();

        Vector2 randomDirection = Vector2.zero;
        while (randomDirection == Vector2.zero)
        {
            randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        Vector2 spawnPosition = center + (randomDirection * (Camera.main.orthographicSize + spawnRadiusBuffer));

        return spawnPosition;
    }
}
