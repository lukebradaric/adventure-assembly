using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManagerBase<T> : MonoBehaviour where T : Entity
{
    public static List<T> Entities { get; private set; } = new List<T>();

    public static void Register(T entity)
    {
        Entities.Add(entity);
    }

    public static void Unregister(T entity)
    {
        Entities.Remove(entity);
    }

    public static T GetNearest(Vector2Int position)
    {
        return GetNearest((Vector2)position);
    }

    public static T GetNearest(Vector2 position)
    {
        if (Entities.Count == 0)
        {
            return null;
        }

        T nearestEntity = null;
        float nearestDistance = float.MaxValue;

        foreach (T entity in Entities)
        {
            float distance = Vector2.Distance(position, entity.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEntity = entity;
            }
        }

        return nearestEntity;
    }

    public static List<T> GetInRadius(Vector2Int position, float radius)
    {
        return GetInRadius((Vector2)position, radius);
    }

    public static List<T> GetInRadius(Vector2 position, float radius)
    {
        List<T> entities = new List<T>();

        foreach (T entity in Entities)
        {
            if (Vector2.Distance(entity.transform.position, position) <= radius)
                entities.Add(entity);
        }

        return entities;
    }

    public static bool TryGet(Vector2Int position, out T entity)
    {
        entity = null;

        foreach (T e in Entities)
        {
            if (e.Position == position)
            {
                entity = e;
                return true;
            }
        }

        return false;
    }

    public static bool TryGet(List<Vector2Int> positions, out T entity)
    {
        entity = null;

        foreach (Vector2Int position in positions)
        {
            if (TryGet(position, out T e))
            {
                entity = e;
                return true;
            }
        }

        return false;
    }
}
