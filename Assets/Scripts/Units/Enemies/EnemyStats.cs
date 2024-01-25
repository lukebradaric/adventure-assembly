namespace AdventureAssembly.Units.Enemies
{
    public class EnemyStats : UnitStats
    {
        public EnemyData EnemyData { get; protected set; }
        public Enemy Enemy { get; protected set; }

        public override void Initialize(Unit unit)
        {
            base.Initialize(unit);
        }
    }
}