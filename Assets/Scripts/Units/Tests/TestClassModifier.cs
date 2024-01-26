using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Modifiers;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Tests
{
    public class TestClassModifier : ClassModifier
    {
        [OdinSerialize] private UnitModifier _unitModifier;

        public override void Apply()
        {
            HeroManager.AddModifier(_unitModifier);
        }

        public override void Remove()
        {
            HeroManager.RemoveModifier(_unitModifier);
        }
    }
}