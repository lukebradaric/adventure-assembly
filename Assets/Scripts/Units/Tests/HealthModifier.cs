using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Modifiers;
using UnityEngine;

namespace AdventureAssembly.Units.Tests
{
    [System.Serializable]
    public class HealthModifier : CharacterUnitModifier
    {
        [SerializeField] private StatModifier<float> _value = new DefaultStatModifier();

        public override void Apply(CharacterUnit unit)
        {
            unit.Stats.MaxHealthMultiplier.AddModifier(_value);
        }

        public override void Remove(CharacterUnit unit)
        {
            unit.Stats.MaxHealthMultiplier.RemoveModifier(_value);
        }
    }
}