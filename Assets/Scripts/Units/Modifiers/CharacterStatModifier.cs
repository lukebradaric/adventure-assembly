using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Reflection;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    /// <summary>
    /// Modifier for adding procceses to a Stat on a Character
    /// </summary>
    public class CharacterStatModifier : CharacterModifier
    {
        [Tooltip("What type of character is this modifier targeting?")]
        [BoxGroup("Target Settings", Order = -1)]
        [OdinSerialize] public CharacterType CharacterType { get; protected set; } = CharacterType.Hero;

        [Tooltip("What is the name of the stat to modify?")]
        [BoxGroup("Modifier")]
        [ShowIf(nameof(CharacterType), CharacterType.Hero)]
        [OdinSerialize] private HeroStatNames _heroStatName;

        [Tooltip("What is the name of the stat to modify?")]
        [BoxGroup("Modifier")]
        [ShowIf(nameof(CharacterType), CharacterType.Enemy)]
        [OdinSerialize] private EnemyStatNames _enemyStatName;

        [Tooltip("What stat process should be applied to the stat?")]
        [BoxGroup("Modifier")]
        [OdinSerialize] private StatProcess _statProcess = new FloatAddStatProcess();

        public override void ApplyToCharacter(Character character)
        {
            // If we are able to get stat property, add modifier
            if (TryGetStatProperty(character, GetStatName(), out Stat<float> stat))
            {
                stat.AddProcess(_statProcess);
            }
        }

        public override void RemoveFromCharacter(Character character)
        {
            if (TryGetStatProperty(character, GetStatName(), out Stat<float> stat))
            {
                stat.RemoveProcess(_statProcess);
            }
        }

        private string GetStatName()
        {
            switch (CharacterType)
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

            PropertyInfo propertyInfo = character.Stats.GetType().GetProperty(statName);
            if (propertyInfo == null)
            {
                Debug.LogError($"Could not find stat of name {statName} on {character.CharacterData.Name}");
                return false;
            }

            object statObject = propertyInfo.GetValue(character.Stats);
            if (statObject is not Stat<float>)
            {
                Debug.LogError($"Could not find Stat<float> using name: {statName}. Modifier was not removed.");
                return false;
            }

            stat = (Stat<float>)statObject;
            return true;
        }

        public override void OnClone(CharacterModifier obj)
        {
            base.OnClone(obj);
            ((CharacterStatModifier)obj)._statProcess = _statProcess.GetClone();
        }
    }
}