using AdventureAssembly.Core.Extensions;
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

        public Stat<float> CriticalChance { get; set; } = new Stat<float>(0f);
        public Stat<float> CriticalMultiplier { get; set; } = new Stat<float>(2f);
        public Stat<float> CriticalBonus { get; set; } = new Stat<float>(0f);
        public Stat<float> AbilitySpeedMultiplier { get; set; } = new Stat<float>(1f);
        public Stat<float> AbilityExecuteBonus { get; protected set; } = new Stat<float>(0f);
        public Stat<float> LuckMultiplier { get; set; } = new Stat<float>(1f);

        public override void Initialize(Character unit)
        {
            base.Initialize(unit);

            this.Hero = (Hero)unit;
            this.HeroData = Hero.HeroData;
        }

        public override DamageData GetDamageData(DamageData damageData)
        {
            damageData = base.GetDamageData(damageData);

            float damage = damageData.Value;

            // Check if critical chance hit
            if (CriticalChance.Value.Chance())
            {
                damageData.IsCritical = true;
            }

            // If target is marked, consume mark to gain guaranteed crit
            if (damageData.Target.StatusEffects.Contains(StatusEffect.Marked))
            {
                damageData.IsCritical = true;
                damageData.Target.StatusEffects.Remove(StatusEffect.Marked);
            }

            // If attack is critical
            if (damageData.IsCritical)
            {
                // Add crit damage bonus
                damage += CriticalBonus.Value;

                // Multiply by crit multiplier
                damage *= CriticalMultiplier.Value;
            }

            // Cast calculated value to integer
            damageData.Value = (int)Mathf.Ceil(damage);

            return damageData;
        }

        public float GetAbilitySpeed()
        {
            // Clamp ability speed to min ability speed
            return Mathf.Max(HeroData.AbilitySpeed / AbilitySpeedMultiplier.Value, MinAbilitySpeed);
        }

        public int GetAbilityExecuteCount()
        {
            return 1 + (int)AbilityExecuteBonus.Value;
        }

        public float GetLuck(float baseLuck)
        {
            return baseLuck * LuckMultiplier.Value;
        }
    }
}