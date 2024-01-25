using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    public class DamageModifier : UnitModifier
    {
        [SerializeField] private StatModifier<float> _statModifier = new DefaultStatModifier();

        public override void Apply(Unit unit)
        {
            unit.Stats.DamageMultiplier.AddModifier(_statModifier);
        }
    }
}