using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManager<T> : MonoBehaviour where T : Entity
{
    protected static List<T> Entities = new List<T>();

    public static void Register(T entity)
    {
        Entities.Add(entity);
    }

    public static void Unregister(T entity)
    {
        Entities.Remove(entity);
    }
}
