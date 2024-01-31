using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class EnemyModifier : UnitModifier
    {
        public override void Apply(CharacterUnit unit)
        {
            Apply((Enemy)unit);
        }

        public abstract void Apply(Enemy enemy);
    }
}
