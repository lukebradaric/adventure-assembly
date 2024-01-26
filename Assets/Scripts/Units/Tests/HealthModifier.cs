using AdventureAssembly.Units.Modifiers;
using UnityEngine;

namespace AdventureAssembly.Units.Tests
{
    [System.Serializable]
    public class HealthModifier : UnitModifier
    {
        [SerializeField] private StatModifier<float> _value = new DefaultStatModifier();

        public override void Apply(Unit unit)
        {
            unit.Stats.MaxHealthMultiplier.AddModifier(_value);
        }

        public override void Remove(Unit unit)
        {
            unit.Stats.MaxHealthMultiplier.RemoveModifier(_value);
        }
    }
}