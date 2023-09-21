using System.Collections.Generic;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class EnemyManager : EntityManager<Enemy>
{
    public static Enemy GetNearestEnemy(Vector2Int position)
    {
        if (Entities.Count == 0)
        {
            return null;
        }

        Enemy nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Enemy enemy in Entities)
        {
            float distance = Vector2.Distance(position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    public static List<Enemy> GetEnemiesInRadius(Vector2Int position, float radius)
    {
        List<Enemy> enemies = new List<Enemy>();

        foreach (Enemy enemy in Entities)
        {
            if (Vector2.Distance(enemy.transform.position, position) <= radius)
                enemies.Add(enemy);
        }

        return enemies;
    }
}
