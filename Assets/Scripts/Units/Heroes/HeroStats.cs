using AdventureAssembly.Units.Characters;
using System;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroStats : CharacterUnitStats
    {
        public HeroData HeroData { get; protected set; }
        public Hero Hero { get; protected set; }

        public override void Initialize(CharacterUnit unit)
        {
            base.Initialize(unit);
        }

        public float GetAbilitySpeed(float baseSpeed)
        {
            return baseSpeed;
        }
    }
}