using System.Collections.Generic;
using UnityEngine;

public static class Vector2IntExtensions
{
    public static List<Vector2Int> GetAdjacents(this Vector2Int vector)
    {
        return new List<Vector2Int>
        {
            new Vector2Int(vector.x+1, vector.y),
            new Vector2Int(vector.x-1, vector.y),
            new Vector2Int(vector.x, vector.y+1),
            new Vector2Int(vector.x, vector.y-1),
        };
    }
}
