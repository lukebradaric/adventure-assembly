using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Modifiers
{
    public abstract class CharacterModifier : CloneObject<CharacterModifier>
    {
        public abstract void ApplyToCharacter(Character character);
        public abstract void RemoveFromCharacter(Character character);
    }
}