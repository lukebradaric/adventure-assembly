using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class UnitModifier
    {
        public abstract void Apply(CharacterUnit unit);
        public abstract void Remove(CharacterUnit unit);
    }
}