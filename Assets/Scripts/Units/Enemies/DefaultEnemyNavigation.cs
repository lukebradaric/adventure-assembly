using AdventureAssembly.Units.Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class DefaultEnemyNavigation : EnemyNavigation
    {
        public override void Update(Enemy enemy)
        {
            // List of possible movement positions
            List<Vector2Int> positions = new List<Vector2Int>()
            {
                new Vector2Int(enemy.Position.x + 1, enemy.Position.y),
                new Vector2Int(enemy.Position.x - 1, enemy.Position.y),
                new Vector2Int(enemy.Position.x, enemy.Position.y + 1),
                new Vector2Int(enemy.Position.x, enemy.Position.y - 1),
            };

            // Get the position of the nearest hero
            Vector2Int nearestHeroPosition = HeroManager.GetNearestUnit(enemy.Position).Position;

            List<Tuple<Vector2Int, float>> positionDistances = new List<Tuple<Vector2Int, float>>();
            float currentDistance = Vector2Int.Distance(enemy.Position, nearestHeroPosition);

            foreach (Vector2Int position in positions)
            {
                float distance = Vector2Int.Distance(position, nearestHeroPosition);

                // Ignore positions that are farther away
                if (distance > currentDistance)
                {
                    continue;
                }

                positionDistances.Add(Tuple.Create(position, distance));
            }

            // Sort the dictionary by distance
            positionDistances = positionDistances.OrderBy(element => element.Item2).ToList();

            Vector2Int? movement = null;
            foreach (var position in positionDistances)
            {
                // If there is a hero in the nearest position, attack it
                if (GridManager.TryGetUnit(position.Item1, out Unit unit) && unit is Hero)
                {
                    enemy.Attack((Hero)unit);
                    return;
                }

                // If the space was empty, set as next movement
                if (!GridManager.HasUnitAtPosition(position.Item1))
                {
                    movement = position.Item1;
                    break;
                }
            }

            // If no valid movement was found, return
            if (movement == null)
            {
                return;
            }

            // Move to best position
            enemy.Move(((Vector2Int)movement) - enemy.Position);
        }
    }
}