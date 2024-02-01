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
        // TODO: Optimize this class by using a dictionar for <Unit, Vector2Int> AND <Vector2Int, Unit>

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

        // Updates a units position in the dictionary
        public static void UpdateUnitPosition(Unit unit, Vector2Int position)
        {
            _units[unit] = position;
        }

        // Returns if there is a unit at the position
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

        // Tries to get a unit at the position
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

        // Returns a random empty position within the bounds
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