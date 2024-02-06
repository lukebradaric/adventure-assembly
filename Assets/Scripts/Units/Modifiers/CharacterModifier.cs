using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class CharacterModifier
    {
        public abstract void Apply(Character character);
        public abstract void Remove(Character character);

        public CharacterModifier GetClone()
        {
            return (CharacterModifier)this.MemberwiseClone();
        }
    }
}