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
        [BoxGroup("Target")]
        [ShowIf(nameof(CharacterType), CharacterType.Hero)]
        [OdinSerialize] public bool SpecificClasses { get; protected set; } = false;

        [BoxGroup("Target")]
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
                    HeroManager.Instance.AddGlobalModifier(this);
                    break;
                case CharacterType.Enemy:
                    EnemyManager.Instance.AddGlobalModifier(this);
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
                    HeroManager.Instance.RemoveGlobalModifier(this);
                    break;
                case CharacterType.Enemy:
                    EnemyManager.Instance.RemoveGlobalModifier(this);
                    break;
            }
        }

        protected override void OnApplyToCharacter(Character character)
        {
            // If targeting specific hero classes
            if (CharacterType == CharacterType.Hero && SpecificClasses)
            {
                if (HeroHasClass((Hero)character))
                {
                    base.OnApplyToCharacter(character);
                }

                return;
            }

            base.OnApplyToCharacter(character);
        }

        protected override void OnRemoveFromCharacter(Character character)
        {
            // If targeting specific hero classes
            if (CharacterType == CharacterType.Hero && SpecificClasses)
            {
                if (HeroHasClass((Hero)character))
                {
                    base.OnRemoveFromCharacter((Hero)character);
                }

                return;
            }

            base.OnRemoveFromCharacter(character);
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