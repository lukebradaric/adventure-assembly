using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public class HealthModifier : UnitModifier
    {
        [SerializeField] private StatModifier<float> _value = new DefaultStatModifier();

        public override void Apply(Unit unit)
        {
            unit.Stats.MaxHealthMultiplier.AddModifier(_value);
        }
    }
}