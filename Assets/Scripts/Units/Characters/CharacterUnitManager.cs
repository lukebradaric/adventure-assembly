using AdventureAssembly.Units.Modifiers;
using System.Collections.Generic;
using System.Linq;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public abstract class CharacterUnitManager<T> : Singleton<CharacterUnitManager<T>> where T : CharacterUnit
    {
        public static List<T> Units { get; protected set; } = new List<T>();

        public static List<CharacterUnitModifier> Modifiers { get; protected set; } = new List<CharacterUnitModifier>();

        protected static void ApplyAllModifiers(T unit)
        {
            // Apply all modifiers to a single unit
            foreach (CharacterUnitModifier modifier in Modifiers)
            {
                modifier.Apply(unit);
            }
        }

        public virtual void AddUnit(T unit)
        {
            Units.Add(unit);
            ApplyAllModifiers(unit);
        }

        public virtual void RemoveUnit(T unit)
        {
            if (!Units.Contains(unit))
            {
                Debug.LogError("Manager does not contain unit!");
            }

            Units.Remove(unit);
        }

        public static void AddModifier(CharacterUnitModifier modifier)
        {
            //Debug.Log($"Adding new modifier: {modifier.GetType().Name}");

            Modifiers.Add(modifier);

            // Apply the new modifier to all existing units
            foreach (T unit in Units)
            {
                modifier.Apply(unit);
            }
        }

        public static void RemoveModifier(CharacterUnitModifier modifier)
        {
            foreach (T unit in Units)
            {
                modifier.Remove(unit);
            }

            Modifiers.Remove(modifier);
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