using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyManager : CharacterManager<Enemy>
    {
        private void OnEnable()
        {
            TickManager.EnemyTickUpdate += OnEnemyTickUpdate;
        }

        private void OnDisable()
        {
            TickManager.EnemyTickUpdate -= OnEnemyTickUpdate;
        }

        private void OnEnemyTickUpdate()
        {
            // Run the navigaton logic for each enemy
            foreach (Enemy enemy in Units)
            {
                enemy.OnNavigate();
            }
        }
    }
}