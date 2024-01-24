using System.Collections.Generic;
using System.Linq;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public abstract class UnitManager<T> : Singleton<UnitManager<T>> where T : Unit
    {
        public static HashSet<T> Units { get; protected set; } = new HashSet<T>();

        public virtual void AddUnit(T unit)
        {
            Units.Add(unit);
        }

        public virtual void RemoveUnit(T unit)
        {
            if (!Units.Contains(unit))
            {
                Debug.LogError("Manager does not contain unit!");
            }

            Units.Remove(unit);
        }

        public static T GetNearestUnit(Vector2Int position)
        {
            T nearestUnit = Units.FirstOrDefault();
            float currentDistance = float.MaxValue;

            foreach (T unit in Units)
            {
                float distance = Vector2Int.Distance(position, unit.Position);
                if (distance < currentDistance)
                {
                    nearestUnit = unit;
                    currentDistance = distance;
                }
            }

            return nearestUnit;
        }

        public static bool HasUnitAtPosition(Vector2Int position)
        {
            // TODO: Optimize this using a dictionary?
            foreach (T unit in Units)
            {
                if (unit.Position == position)
                {
                    return true;
                }
            }

            return false;
        }
    }
}