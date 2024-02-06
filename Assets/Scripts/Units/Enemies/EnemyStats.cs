using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyStats : CharacterUnitStats
    {
        public EnemyData EnemyData { get; protected set; }
        public Enemy Enemy { get; protected set; }

        public Stat<float> GoldDropChance { get; set; } = new Stat<float>(0.01f);

        public override void Initialize(CharacterUnit unit)
        {
            base.Initialize(unit);
        }
    }
}