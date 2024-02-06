using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    /// <summary>
    /// Modifier for adding procceses to a Stat on a Character
    /// </summary>
    public class CharacterStatModifier : CharacterModifier
    {
        [SerializeField] private CharacterType _targetType;

        [ShowIf(nameof(_targetType), CharacterType.Hero)]
        [SerializeField] private HeroStatNames _heroStatName;

        [ShowIf(nameof(_targetType), CharacterType.Enemy)]
        [SerializeField] private EnemyStatNames _enemyStatName;

        [SerializeField] private StatProcess _statProcess = new FloatAddStatProcess();

        public override void Apply(Character character)
        {
            // If we are able to get stat property, add modifier
            if (TryGetStatProperty(character, GetStatName(), out Stat<float> stat))
            {
                stat.AddProcess(_statProcess);
            }
        }

        public override void Remove(Character character)
        {
            if (TryGetStatProperty(character, GetStatName(), out Stat<float> stat))
            {
                stat.AddProcess(_statProcess);
            }
        }

        /// <summary>
        /// Returns the stat name based on selected CharacterType.
        /// </summary>
        /// <returns></returns>
        private string GetStatName()
        {
            switch (_targetType)
            {
                case CharacterType.Hero:
                    return _heroStatName.ToString();
                case CharacterType.Enemy:
                    return _enemyStatName.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Tries to get a Stat property from a Character.
        /// </summary>
        /// <param name="character">The character to get the stat from</param>
        /// <param name="statName">The name of the stat to get</param>
        /// <param name="stat">The stat object returned if it is found</param>
        /// <returns></returns>
        private bool TryGetStatProperty(Character character, string statName, out Stat<float> stat)
        {
            stat = null;

            Debug.Log(character.Stats.GetType().GetProperty(statName).Name);

            object statObject = character.Stats.GetType().GetProperty(statName).GetValue(character.Stats);
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