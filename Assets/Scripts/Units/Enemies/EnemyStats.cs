using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Stats;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyStats : CharacterStats
    {
        public EnemyData EnemyData { get; protected set; }
        public Enemy Enemy { get; protected set; }

        public Stat<float> GoldDropChance { get; set; } = new Stat<float>(0.01f);

        public override void Initialize(Character unit)
        {
            base.Initialize(unit);
        }
    }
}