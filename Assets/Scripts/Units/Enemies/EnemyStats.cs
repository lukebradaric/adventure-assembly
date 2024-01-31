using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyStats : CharacterUnitStats
    {
        public EnemyData EnemyData { get; protected set; }
        public Enemy Enemy { get; protected set; }

        public override void Initialize(CharacterUnit unit)
        {
            base.Initialize(unit);
        }
    }
}