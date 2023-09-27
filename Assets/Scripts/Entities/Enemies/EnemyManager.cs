using System.Collections.Generic;
using TinyTools.AutoLoad;
using UnityEngine;

public class EnemyManager : EntityManagerBase<Enemy>
{
    private static HashSet<Vector2Int> _requestedPositions = new HashSet<Vector2Int>();

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
        _requestedPositions.Clear();
    }

    public static bool IsPositionTaken(Vector2Int position)
    {
        return _requestedPositions.Contains(position);
    }

    public static bool RequestPosition(Vector2Int position)
    {
        // If position already requested, return
        if (IsPositionTaken(position))
        {
            return false;
        }

        // If enemy already in position, return
        foreach (Enemy enemy in Entities)
        {
            if (enemy.Position == position)
            {
                return false;
            }
        }

        _requestedPositions.Add(position);
        return true;
    }
}
