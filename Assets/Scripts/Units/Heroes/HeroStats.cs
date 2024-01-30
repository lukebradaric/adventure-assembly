using System;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroStats : UnitStats
    {
        public HeroData HeroData { get; protected set; }
        public Hero Hero { get; protected set; }

        public override void Initialize(Unit unit)
        {
            base.Initialize(unit);
        }

        public float GetAbilitySpeed(float baseSpeed)
        {
            return baseSpeed;
        }
    }
}