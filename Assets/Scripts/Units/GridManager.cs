using System.Collections.Generic;
using TinyTools.Extensions;
using UnityEngine;

namespace AdventureAssembly.Units
{
    /// <summary>
    /// Handles all of the Units and their position on the grid.
    /// </summary>
    public class GridManager : MonoBehaviour
    {
        // Dictionary of each unit and their position
        private static Dictionary<Unit, Vector2Int> _units = new Dictionary<Unit, Vector2Int>();

        private void Start()
        {
            _units.Clear();
        }

        // Add a unit to the dictionary with their position
        public static void AddUnit(Unit unit, Vector2Int position)
        {
            if (HasUnitAtPosition(position))
            {
                Debug.LogError("There is already a unit at this position!");
                return;
            }

            _units.Add(unit, position);
        }

        // Remove a unit from the dictionary
        public static void RemoveUnit(Unit unit)
        {
            if (!_units.ContainsKey(unit))
            {
                Debug.LogError("This unit was not found in the dictionary!");
                return;
            }

            _units.Remove(unit);
        }

        /// <summary>
        /// Updates a units position on the grid.
        /// </summary>
        /// <param name="unit">The unit to update</param>
        /// <param name="position">The new position</param>
        public static void UpdateUnitPosition(Unit unit, Vector2Int position)
        {
            _units[unit] = position;
        }

        /// <summary>
        /// Returns true if there is a unit at the position
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns></returns>
        public static bool HasUnitAtPosition(Vector2Int position)
        {
            foreach (Vector2Int pos in _units.Values)
            {
                if (position == pos)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to get a unit from a positon
        /// </summary>
        /// <param name="position">The position to check for a unit</param>
        /// <param name="unit">The unit found, Null if no unit was found</param>
        /// <returns></returns>
        public static bool TryGetUnit(Vector2Int position, out Unit unit)
        {
            unit = null;

            foreach (KeyValuePair<Unit, Vector2Int> pair in _units)
            {
                if (pair.Value == position)
                {
                    unit = pair.Key;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a random empty position on the grid
        /// </summary>
        /// <param name="bounds">The boundaries of the grid to check</param>
        /// <returns></returns>
        public static Vector2Int GetRandomEmptyPosition(Vector4 bounds)
        {
            List<Vector2Int> positions = new List<Vector2Int>();

            for (int x = (int)bounds.x; x < (int)bounds.y; x++)
            {
                for (int y = (int)bounds.z; y < (int)bounds.w; y++)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }

            foreach (Vector2Int takenPosition in _units.Values)
            {
                positions.Remove(takenPosition);
            }

            return positions.Random();
        }
    }
}