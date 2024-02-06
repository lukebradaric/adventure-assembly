using AdventureAssembly.Units.Characters;
using System;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroStats : CharacterStats
    {
        public HeroData HeroData { get; protected set; }
        public Hero Hero { get; protected set; }

        public override void Initialize(Character unit)
        {
            base.Initialize(unit);
        }

        public float GetAbilitySpeed(float baseSpeed)
        {
            return baseSpeed;
        }
    }
}