using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class CharacterUnitModifier
    {
        public abstract void Apply(CharacterUnit unit);
        public abstract void Remove(CharacterUnit unit);

        public CharacterUnitModifier GetClone()
        {
            return (CharacterUnitModifier)this.MemberwiseClone();
        }
    }
}