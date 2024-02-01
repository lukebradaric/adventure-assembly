using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class LinearEnemyNavigation : EnemyNavigation
    {
        // The direction this enemy is moving in. This will never change once initialized.
        private Vector2Int? _direction = null;

        // Try to change the spawn position of this unit
        private void TryChangeSpawn(Enemy enemy)
        {
            if (GridManager.HasUnitAtPosition(new Vector2Int(16, 0)))
            {
                return;
            }

            enemy.SetPosition(new Vector2Int(16, 0));
            _direction = Vector2Int.left;
        }

        public override void Update(Enemy enemy)
        {
            // If a direction has not been calculated, continue trying to calculate direction
            if (_direction == null)
            {
                TryChangeSpawn(enemy);
                return;
            }

            // If there is a unit in this enemies path, move in a random possible direction
            if (GridManager.HasUnitAtPosition(enemy.Position + (Vector2Int)_direction))
            {
                List<Vector2Int> directions = EnemyNavigation.GetMovementDirections();
                foreach (Vector2Int direction in directions)
                {
                    if (!GridManager.HasUnitAtPosition(enemy.Position + direction))
                    {
                        enemy.Move(direction);
                        break;
                    }
                }

                return;
            }

            // If nothing was in the way, continue moving straight
            enemy.Move((Vector2Int)_direction);
        }
    }
}