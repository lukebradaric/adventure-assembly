using System.Collections.Generic;
using TinyTools.Extensions;
using UnityEngine;

namespace AdventureAssembly.Core
{
    public class GridManager : MonoBehaviour
    {
        private static Dictionary<Vector2Int, GameObject> _units = new Dictionary<Vector2Int, GameObject>();

        // Add a position for the first time to the dictionary
        public static void AddPosition(Vector2Int position, GameObject gameObject)
        {
            if (_units.ContainsKey(position))
            {
                Debug.LogError("Grid already contains gameobject at this position!");
                return;
            }

            _units.Add(position, gameObject);
        }

        // Removes a position from the dictionary
        public static void RemovePosition(Vector2Int position, GameObject gameObject)
        {
            if (!_units.ContainsKey(position))
            {
                Debug.LogError("There is no gameobject at this position to remove");
                return;
            }

            if (_units[position] != gameObject)
            {
                Debug.LogError("The object you are trying to move is not the one at this position!");
                return;
            }

            _units.Remove(position);
        }

        // Updates the position of a gameobject on the grid
        public static void UpdatePosition(Vector2Int lastPosition, Vector2Int newPosition, GameObject gameObject)
        {
            // If there was a unit at the last position, remove it
            // TODO: Maybe check if that unit at the last position was the same as the one we are adding
            if (_units.ContainsKey(lastPosition))
            {
                _units.Remove(lastPosition);
            }
            _units.Add(newPosition, gameObject);
        }

        // Returns true if there is a gameobject at the position
        public static bool IsObjectAtPosition(Vector2Int position)
        {
            return _units.ContainsKey(position);
        }

        // Tries to get a gameobject from a position
        public static bool TryGetObject(Vector2Int position, out GameObject gameObject)
        {
            gameObject = null;

            if (!_units.ContainsKey(position))
            {
                return false;
            }

            gameObject = _units[position];
            return true;
        }

        // Returns a random empty position on the grid
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

            foreach (Vector2Int takenPosition in _units.Keys)
            {
                positions.Remove(takenPosition);
            }

            return positions.Random();
        }
    }
}