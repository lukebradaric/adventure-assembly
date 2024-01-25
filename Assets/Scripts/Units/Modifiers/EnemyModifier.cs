using AdventureAssembly.Units.Enemies;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class EnemyModifier : UnitModifier
    {
        public override void Apply(Unit unit)
        {
            Apply((Enemy)unit);
        }

        public abstract void Apply(Enemy enemy);
    }
}
