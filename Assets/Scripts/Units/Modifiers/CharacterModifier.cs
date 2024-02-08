using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Modifiers
{
    public abstract class CharacterModifier : CloneObject<CharacterModifier>
    {
        public abstract void Apply(Character character);
        public abstract void Remove(Character character);
    }
}