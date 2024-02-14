using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Classes;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    public class GlobalCharacterStatModifier : CharacterStatModifier
    {
        [Tooltip("Should this hero modifier only be applied to certain classes?")]
        [BoxGroup("Target Settings")]
        [ShowIf(nameof(CharacterType), CharacterType.Hero)]
        [OdinSerialize] public bool SpecificClasses { get; protected set; } = false;

        [BoxGroup("Target Settings")]
        [ShowIf(nameof(_showClassList))]
        [OdinSerialize] public List<ClassData> ClassData { get; protected set; } = new List<ClassData>();

        private bool _showClassList => CharacterType == CharacterType.Hero && SpecificClasses;

        /// <summary>
        /// Applies this modifiers to all characters of the target type.
        /// </summary>
        public void Apply()
        {
            switch (CharacterType)
            {
                case CharacterType.Hero:
                    HeroManager.Instance.AddModifierToAll(this);
                    break;
                case CharacterType.Enemy:
                    EnemyManager.Instance.AddModifierToAll(this);
                    break;
            }
        }

        /// <summary>
        /// Removes this modifiers from all characters of the target type.
        /// </summary>
        public void Remove()
        {
            switch (CharacterType)
            {
                case CharacterType.Hero:
                    HeroManager.Instance.RemoveModifierFromAll(this);
                    break;
                case CharacterType.Enemy:
                    EnemyManager.Instance.RemoveModifierFromAll(this);
                    break;
            }
        }

        public override void ApplyToCharacter(Character character)
        {
            // If targeting specific hero classes
            if (CharacterType == CharacterType.Hero && SpecificClasses)
            {
                if (HeroHasClass((Hero)character))
                {
                    base.ApplyToCharacter(character);
                }

                return;
            }

            base.ApplyToCharacter(character);
        }

        public override void RemoveFromCharacter(Character character)
        {
            // If targeting specific hero classes
            if (CharacterType == CharacterType.Hero && SpecificClasses)
            {
                if (HeroHasClass((Hero)character))
                {
                    base.RemoveFromCharacter((Hero)character);
                }

                return;
            }

            base.RemoveFromCharacter(character);
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

            return false;
        }
    }
}