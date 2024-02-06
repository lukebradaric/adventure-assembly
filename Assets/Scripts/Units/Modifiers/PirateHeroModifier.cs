using AdventureAssembly.Core;
using AdventureAssembly.Units.Heroes;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    internal class PirateStatModifier : StatModifier<float>
    {
        private float _damageMultiplierPerGold;

        public PirateStatModifier(float multiplier)
        {
            _damageMultiplierPerGold = multiplier;
        }

        public override float Process(float value)
        {
            return value * (1 + (GoldManager.CurrentGold.Value * _damageMultiplierPerGold));
        }
    }

    public class PirateHeroModifier : HeroModifier
    {
        [SerializeField] private float _damageMultiplierPerGold;

        private PirateStatModifier _modifier;

        public override void Apply(Hero hero)
        {
            if (_modifier == null)
            {
                _modifier = new PirateStatModifier(_damageMultiplierPerGold);
            }

            hero.Stats.DamageMultiplier.AddModifier(_modifier);
        }

        public override void Remove(Hero hero)
        {
            if (_modifier == null)
            {
                return;
            }

            hero.Stats.DamageMultiplier.RemoveModifier(_modifier);
        }
    }
}