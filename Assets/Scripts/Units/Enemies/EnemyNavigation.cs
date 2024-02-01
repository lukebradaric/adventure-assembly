using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public abstract class EnemyNavigation
    {
        public abstract void Update(Enemy enemy);

        /// <summary>
        /// Creates a list of all movement directions. Does not check for collision.
        /// </summary>
        /// <returns></returns>
        public static List<Vector2Int> GetMovementDirections()
        {
            return new List<Vector2Int>()
            {
                Vector2Int.up,
                Vector2Int.right,
                Vector2Int.down,
                Vector2Int.left,
            };
        }

        public EnemyNavigation GetClone()
        {
            return (EnemyNavigation)this.MemberwiseClone();
        }
    }
}