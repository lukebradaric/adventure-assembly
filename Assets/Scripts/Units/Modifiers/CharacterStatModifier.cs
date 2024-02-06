using AdventureAssembly.Units.Characters;
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
        [Tooltip("What is the name of the stat to modify?")]
        [BoxGroup("Modifier")]
        [OdinSerialize] private StatNames _statName;

        [Tooltip("What stat process should be applied to the stat?")]
        [BoxGroup("Modifier")]
        [OdinSerialize] private StatProcess _statProcess = new FloatAddStatProcess();

        public override void Apply(Character character)
        {
            // If we are able to get stat property, add modifier
            if (TryGetStatProperty(character, _statName.ToString(), out Stat<float> stat))
            {
                stat.AddProcess(_statProcess);
            }
        }

        public override void Remove(Character character)
        {
            if (TryGetStatProperty(character, _statName.ToString(), out Stat<float> stat))
            {
                stat.RemoveProcess(_statProcess);
            }
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
    }
}