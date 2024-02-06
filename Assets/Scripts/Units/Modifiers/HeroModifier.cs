using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Heroes;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class HeroModifier : CharacterUnitModifier
    {
        public override void Apply(CharacterUnit unit)
        {
            Apply((Hero)unit);
        }

        public abstract void Apply(Hero hero);

        public override void Remove(CharacterUnit unit)
        {
            Remove((Hero)unit);
        }

        public abstract void Remove(Hero hero);
    }
}
