using AdventureAssembly.Core;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyManager : UnitManager<Enemy>
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
                enemy.EnemyData.Navigation?.Update(enemy);
            }
        }
    }
}