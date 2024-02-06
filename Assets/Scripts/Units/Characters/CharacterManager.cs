using AdventureAssembly.Units.Modifiers;
using System.Collections.Generic;
using System.Linq;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public abstract class CharacterManager<T> : Singleton<CharacterManager<T>> where T : Character
    {
        public static List<T> Units { get; protected set; } = new List<T>();

        public static List<CharacterModifier> Modifiers { get; protected set; } = new List<CharacterModifier>();

        protected static void ApplyAllModifiers(T unit)
        {
            // Apply all modifiers to a single unit
            foreach (CharacterModifier modifier in Modifiers)
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

        /// <summary>
        /// Adds a modifier to all units and future units in this manager.
        /// </summary>
        /// <param name="modifier">The modifier to add.</param>
        public static void AddGlobalModifier(CharacterModifier modifier)
        {
            //Debug.Log($"Adding new modifier: {modifier.GetType().Name}");

            Modifiers.Add(modifier);

            // Apply the new modifier to all existing units
            foreach (T unit in Units)
            {
                modifier.Apply(unit);
            }
        }

        ///// <summary>
        ///// Adds a list of modifiers to all units and future units in this manager.
        ///// </summary>
        ///// <param name="modifiers">The list of modifiers to add.</param>
        //public static void AddGlobalModifiers(List<CharacterModifier> modifiers)
        //{
        //    foreach (CharacterModifier modifier in modifiers)
        //    {
        //        AddGlobalModifier(modifier);
        //    }
        //}

        /// <summary>
        /// Removes a modifier from all units and future units in this manager.
        /// </summary>
        /// <param name="modifier">The modifier to remove. Must be a reference to the modifier that was applied.</param>
        public static void RemoveGlobalModifier(CharacterModifier modifier)
        {
            foreach (T unit in Units)
            {
                modifier.Remove(unit);
            }

            Modifiers.Remove(modifier);
        }

        ///// <summary>
        ///// Removes a list of modifiers from all units and future units in this manager.
        ///// </summary>
        ///// <param name="modifiers">The list of modifiers to remove. Must be references to the modifiers that were applied.</param>
        //public static void RemoveGlobalModifiers(List<CharacterModifier> modifiers)
        //{
        //    foreach (CharacterModifier modifier in modifiers)
        //    {
        //        RemoveGlobalModifier(modifier);
        //    }
        //}

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