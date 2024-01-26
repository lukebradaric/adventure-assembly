using AdventureAssembly.Units.Modifiers;
using UnityEngine;

namespace AdventureAssembly.Units.Tests
{
    public class DamageModifier : UnitModifier
    {
        [SerializeField] private StatModifier<float> _statModifier = new DefaultStatModifier();

        public override void Apply(Unit unit)
        {
            unit.Stats.DamageMultiplier.AddModifier(_statModifier);
        }

        public override void Remove(Unit unit)
        {
            unit.Stats.DamageMultiplier.RemoveModifier(_statModifier);
        }
    }
}