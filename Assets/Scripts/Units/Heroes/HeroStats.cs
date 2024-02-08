using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Stats;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroStats : CharacterStats
    {
        private const float MinAbilitySpeed = 0.1f;

        public HeroData HeroData { get; protected set; }
        public Hero Hero { get; protected set; }

        public Stat<float> AbilityExecuteBonus { get; protected set; } = new Stat<float>(0f);

        public override void Initialize(Character unit)
        {
            base.Initialize(unit);

            this.Hero = (Hero)unit;
            this.HeroData = Hero.HeroData;
        }

        public float GetAbilitySpeed()
        {
            // Clamp ability speed to min ability speed
            return Mathf.Max(HeroData.AbilitySpeed, MinAbilitySpeed);
        }
    }
}