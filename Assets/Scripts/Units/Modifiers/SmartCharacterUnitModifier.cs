using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Tests;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    public class SmartCharacterUnitModifier : CharacterUnitModifier
    {
        [SerializeField] private CharacterUnitType _targetType;

        [ShowIf(nameof(_targetType), CharacterUnitType.Hero)]
        [SerializeField] private HeroStatNames _heroStatName;

        [ShowIf(nameof(_targetType), CharacterUnitType.Enemy)]
        [SerializeField] private EnemyStatNames _enemyStatName;

        [SerializeField] private float _value = 0;

        private DefaultStatModifier _modifier = null;

        public override void Apply(CharacterUnit unit)
        {
            // If we are able to get stat property, add modifier
            if (TryGetStatProperty(unit, GetStatName(), out Stat<float> stat))
            {
                // If the modifier has not been creted yet, create using default
                if (_modifier == null)
                {
                    _modifier = new DefaultStatModifier(_value);
                }

                stat.AddModifier(_modifier);
            }
        }

        public override void Remove(CharacterUnit unit)
        {
            if (_modifier == null)
            {
                return;
            }

            if (TryGetStatProperty(unit, GetStatName(), out Stat<float> stat))
            {
                stat.RemoveModifier(_modifier);
            }
        }

        /// <summary>
        /// Returns the stat name based on selected CharacterUnitType.
        /// </summary>
        /// <returns></returns>
        private string GetStatName()
        {
            switch (_targetType)
            {
                case CharacterUnitType.Hero:
                    return _heroStatName.ToString();
                case CharacterUnitType.Enemy:
                    return _enemyStatName.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Tries to get a Stat property from a CharacterUnit.
        /// </summary>
        /// <param name="unit">The unit to get the stat from</param>
        /// <param name="statName">The name of the stat to get</param>
        /// <param name="stat">The stat object returned if it is found</param>
        /// <returns></returns>
        private bool TryGetStatProperty(CharacterUnit unit, string statName, out Stat<float> stat)
        {
            stat = null;

            Debug.Log(unit.Stats.GetType().GetProperty(statName).Name);

            object statObject = unit.Stats.GetType().GetProperty(statName).GetValue(unit.Stats);
            if (statObject is not Stat<float>)
            {
                Debug.LogError($"Could not find Stat<float> using name: {statName}. Modifier was not removed.");
                return false;
            }

            stat = (Stat<float>)statObject;
            return true;
        }
    }
}