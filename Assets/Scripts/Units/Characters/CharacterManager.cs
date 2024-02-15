using AdventureAssembly.Units.Modifiers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public abstract class CharacterManager<T> : Singleton<CharacterManager<T>> where T : Character
    {
        public List<T> Units { get; protected set; } = new List<T>();

        public List<CharacterModifier> Modifiers { get; protected set; } = new List<CharacterModifier>();

        protected void ApplyAllModifiers(T unit)
        {
            // Apply all modifiers to a single unit
            foreach (CharacterModifier modifier in Modifiers)
            {
                modifier.ApplyToCharacter(unit);
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
        public void AddModifierToAll(CharacterModifier modifier)
        {
            // If modifier is global and temporary, start remove coroutine
            if (modifier is GlobalCharacterStatModifier && ((GlobalCharacterStatModifier)(modifier)).IsTemporary)
            {
                StartCoroutine(RemoveModifierCoroutine(modifier, modifier.Duration));
            }

            Modifiers.Add(modifier);

            // Apply the new modifier to all existing units
            foreach (T unit in Units)
            {
                modifier.ApplyToCharacter(unit);
            }
        }

        /// <summary>
        /// Removes a modifier from all units and future units in this manager.
        /// </summary>
        /// <param name="modifier">The modifier to remove. Must be a reference to the modifier that was applied.</param>
        public void RemoveModifierFromAll(CharacterModifier modifier)
        {
            foreach (T unit in Units)
            {
                modifier.RemoveFromCharacter(unit);
            }

            Modifiers.Remove(modifier);
        }

        private IEnumerator RemoveModifierCoroutine(CharacterModifier modifier, float duration)
        {
            yield return new WaitForSeconds(duration);

            // If the modifier wasn't already removed, remove from all
            if (Modifiers.Contains(modifier))
            {
                RemoveModifierFromAll(modifier);
            }
        }

        /// <summary>
        /// Returns the unit nearest to a position, null if none found.
        /// </summary>
        /// <param name="position">The position to check from</param>
        /// <param name="visibleUnitsOnly">Should the search include only visible units?</param>
        /// <returns></returns>
        public T GetNearestUnit(Vector2Int position, bool visibleUnitsOnly = true)
        {
            T nearestUnit = null;
            float currentDistance = float.MaxValue;

            foreach (T unit in Units)
            {
                // If the unit is not visible on screen, ignore
                if (visibleUnitsOnly && !unit.SpriteRenderer.isVisible)
                {
                    continue;
                }

                float distance = Vector2Int.Distance(position, unit.Position);
                if (distance < currentDistance)
                {
                    nearestUnit = unit;
                    currentDistance = distance;
                }
            }

            return nearestUnit;
        }

        /// <summary>
        /// Returns a list of units within a radius from a position.
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <param name="radius">The radius to check</param>
        /// <returns></returns>
        public List<T> GetUnitsInRadius(Vector2 position, float radius)
        {
            List<T> units = new List<T>();

            foreach (T unit in Units)
            {
                if (Vector2.Distance(position, unit.Position) <= radius)
                {
                    units.Add(unit);
                }
            }

            return units;

        }

        public bool HasUnitAtPosition(Vector2Int position)
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