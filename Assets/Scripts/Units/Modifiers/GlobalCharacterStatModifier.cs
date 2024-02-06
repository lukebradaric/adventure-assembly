using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Classes;
using AdventureAssembly.Units.Heroes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    public class GlobalCharacterStatModifier : CharacterStatModifier
    {
        [Tooltip("What type of character is this modifier targeting?")]
        [BoxGroup("Target Settings", Order = -1)]
        [OdinSerialize] public CharacterType CharacterType { get; protected set; } = CharacterType.Hero;

        [Tooltip("Should this hero modifier only be applied to certain classes?")]
        [BoxGroup("Target Settings")]
        [ShowIf(nameof(CharacterType), CharacterType.Hero)]
        [OdinSerialize] public bool SpecificClasses { get; protected set; } = false;

        [BoxGroup("Target Settings")]
        [ShowIf(nameof(_showClassList))]
        [OdinSerialize] public List<ClassData> ClassData { get; protected set; } = new List<ClassData>();

        private bool _showClassList => CharacterType == CharacterType.Hero && SpecificClasses;

        public override void Apply(Character character)
        {
            // If targeting specific hero classes
            if (CharacterType == CharacterType.Hero && SpecificClasses)
            {
                if (HeroHasClass((Hero)character))
                {
                    base.Apply(character);
                }

                return;
            }

            base.Apply(character);
        }

        public override void Remove(Character character)
        {
            // If targeting specific hero classes
            if (CharacterType == CharacterType.Hero && SpecificClasses)
            {
                if (HeroHasClass((Hero)character))
                {
                    base.Remove((Hero)character);
                }

                return;
            }

            base.Remove(character);
        }

        /// <summary>
        /// Returns true if the hero has a class from this list.
        /// </summary>
        /// <param name="hero">The hero to check</param>
        /// <returns></returns>
        private bool HeroHasClass(Hero hero)
        {
            foreach (ClassData classData in ClassData)
            {
                if (hero.HeroData.ClassData.Contains(classData))
                {
                    return true;
                }
            }

            Debug.Log($"{hero.name} will not have modifier applied!");
            return false;
        }
    }
}