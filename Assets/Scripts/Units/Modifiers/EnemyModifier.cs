using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class EnemyModifier : CharacterUnitModifier
    {
        public override void Apply(CharacterUnit unit)
        {
            Apply((Enemy)unit);
        }

        public abstract void Apply(Enemy enemy);

        public override void Remove(CharacterUnit unit)
        {
            Remove((Enemy)unit);
        }

        public abstract void Remove(Enemy enemy);
    }
}
