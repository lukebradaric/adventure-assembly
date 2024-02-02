using AdventureAssembly.Core;
using AdventureAssembly.Units.Enemies;

namespace AdventureAssembly.Units.Modifiers
{
    /// <summary>
    /// EnemyModifier that will spawn gold whenever an enemy takes damage.
    /// </summary>
    public class GoldenEnemyModifier : EnemyModifier
    {
        public override void Apply(Enemy enemy)
        {
            enemy.Damaged += OnDamaged;
        }

        public override void Remove(Enemy enemy)
        {
            enemy.Damaged -= OnDamaged;
        }

        private void OnDamaged(DamageData data)
        {
            GoldManager.Instance.AddGold(data.Target.transform.position, data.Value);
        }
    }
}