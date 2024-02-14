using Sirenix.OdinInspector;
using System.Collections.Generic;
using TinyTools.Extensions;
using TinyTools.Generics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AdventureAssembly.Units
{
    /// <summary>
    /// Used to draw the boundaries of the map for player movement and enemy spawning.
    /// Contains functions to check if positions are within valid boundaries.
    /// Positions must be drawn and baked for each level.
    /// </summary>
    public class LevelMap : Singleton<LevelMap>
    {
        [BoxGroup("Components")]
        [SerializeField] private Tilemap _tilemap;
        [BoxGroup("Components")]
        [SerializeField] private TileBase _playerTileBase;
        [BoxGroup("Components")]
        [SerializeField] private TileBase _enemyTileBase;

        [BoxGroup("Data")]
        [SerializeField] private List<Vector2Int> _playerPositions;
        [BoxGroup("Data")]
        [SerializeField] private List<Vector2Int> _enemyPositions;

        [BoxGroup("Tools")]
        [Button]
        private void Bake()
        {
            _playerPositions.Clear();
            _enemyPositions.Clear();

            foreach (var position in _tilemap.cellBounds.allPositionsWithin)
            {
                TileBase tileBase = _tilemap.GetTile(position);

                if (tileBase == null)
                {
                    continue;
                }

                if (tileBase == _playerTileBase)
                {
                    _playerPositions.Add((Vector2Int)position);
                    continue;
                }

                if (tileBase == _enemyTileBase)
                {
                    _enemyPositions.Add((Vector2Int)position);
                    continue;
                }
            }
        }

        [BoxGroup("Tools")]
        [Button]
        private void Clear()
        {
            _playerPositions.Clear();
            _enemyPositions.Clear();
        }

        [BoxGroup("Tools")]
        [Button]
        private void Show()
        {
            _tilemap.gameObject.SetActive(true);
        }

        [BoxGroup("Tools")]
        [Button]
        private void Hide()
        {
            _tilemap.gameObject.SetActive(false);
        }

        /// <summary>
        /// Returns a new list of all positions in the player map.
        /// </summary>
        /// <returns></returns>
        public List<Vector2Int> GetPlayerMapPositions()
        {
            return new List<Vector2Int>(_playerPositions);
        }

        /// <summary>
        /// Returns if the position is within the player map positions.
        /// </summary>
        /// <param name="position">The position to check</param>
        /// <returns></returns>
        public bool IsWithinPlayerMap(Vector2Int position)
        {
            return _playerPositions.Contains(position);
        }

        /// <summary>
        /// Returns a random position within the player map.
        /// </summary>
        /// <returns></returns>
        public Vector2Int GetRandomPositionWithinPlayerMap()
        {
            return _playerPositions.Random();
        }

        /// <summary>
        /// Returns a random position witin the enemy map.
        /// </summary>
        /// <returns></returns>
        public Vector2Int GetRandomPositionWithinEnemyMap()
        {
            return _enemyPositions.Random();
        }
    }
}