using AdventureAssembly.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public static HashSet<Enemy> Enemies = new HashSet<Enemy>();

        public static void Register(Enemy enemy)
        {
            Enemies.Add(enemy);
        }

        public static void Unregister(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }

        public static bool IsEnemyAtPosition(Vector2Int position)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Position == position)
                {
                    return true;
                }
            }

            return false;
        }

        private void OnEnable()
        {
            TickManager.EnemyTickUpdate += OnEnemyTickUpdate;
        }

        private void OnDisable()
        {
            TickManager.EnemyTickUpdate -= OnEnemyTickUpdate;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("h"))
            {

            }
        }

        private void OnEnemyTickUpdate()
        {
            // Run the navigaton logic for each enemy
            foreach (Enemy enemy in Enemies)
            {
                enemy.EnemyData.Navigation?.Update(enemy);
            }
        }
    }
}