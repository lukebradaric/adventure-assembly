using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Tests;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    public class SmartHeroModifier : HeroModifier
    {
        [SerializeField] private HeroStatNames _statName;
        [SerializeField] private float _value = 0;

        private DefaultStatModifier _modifier = null;
        private Stat<float> _stat = null;

        public override void Apply(Hero hero)
        {
            // Try to find a stat on the hero based on enum name
            object statObject = hero.Stats.GetType().GetProperty(_statName.ToString()).GetValue(hero.Stats);
            if (statObject is not Stat<float>)
            {
                Debug.LogError($"Could not find Stat<float> using name: {_statName}. Modifier was not applied.");
                return;
            }

            // Get stat and create modifier
            _stat = (Stat<float>)statObject;
            _modifier = new DefaultStatModifier(_value);

            // Add modifier to the found stat
            _stat.AddModifier(_modifier);
        }

        public override void Remove(CharacterUnit unit)
        {
            if (_modifier == null || _stat == null)
            {
                return;
            }

            _stat.RemoveModifier(_modifier);
        }
    }
}