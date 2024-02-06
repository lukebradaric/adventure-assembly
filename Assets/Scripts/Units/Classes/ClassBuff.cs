using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Classes
{
    [System.Serializable]
    public class ClassBuff
    {
        [BoxGroup("Settings")]
        [Tooltip("How many Heroes of this class type are required for this buff to be active?")]
        [OdinSerialize] public int RequiredCount { get; protected set; }

        [BoxGroup("Modifiers")]
        [Tooltip("List of modifiers that will be applied.")]
        [OdinSerialize] public List<GlobalCharacterStatModifier> GlobalCharacterModifiers { get; protected set; } = new List<GlobalCharacterStatModifier>();

        //[BoxGroup("Settings")]
        //[Tooltip("Do you also want the option to add modifiers to enemies using this class?")]
        //[SerializeField] private bool _enableEnemyModifiers = false;

        //[BoxGroup("Class Settings")]
        //[Tooltip("Do you want to apply this buff to only specific classes?")]
        //[SerializeField] private bool _specificClasses;

        //[BoxGroup("Class Settings")]
        //[Tooltip("What classes should this buff be applied to?")]
        //[ShowIf(nameof(_specificClasses))]
        //[SerializeField] private List<ClassData> _classes = new List<ClassData>();

        //[BoxGroup("Hero Modifiers")]
        //[SerializeField] private List<CharacterModifier> _heroModifiers = new List<CharacterModifier>();

        //[BoxGroup("Enemy Modifiers")]
        //[ShowIf(nameof(_enableEnemyModifiers))]
        //[SerializeField] private List<CharacterModifier> _enemyModifiers = new List<CharacterModifier>();

        public virtual void Apply()
        {
            foreach (GlobalCharacterStatModifier modifier in GlobalCharacterModifiers)
            {
                if (modifier.CharacterType == CharacterType.Hero)
                {
                    HeroManager.AddGlobalModifier(modifier);
                }
                else if (modifier.CharacterType == CharacterType.Enemy)
                {
                    EnemyManager.AddGlobalModifier(modifier);
                }
            }
        }

        public virtual void Remove()
        {
            foreach (GlobalCharacterStatModifier modifier in GlobalCharacterModifiers)
            {
                if (modifier.CharacterType == CharacterType.Hero)
                {
                    HeroManager.RemoveGlobalModifier(modifier);
                }
                else if (modifier.CharacterType == CharacterType.Enemy)
                {
                    EnemyManager.RemoveGlobalModifier(modifier);
                }
            }
        }

        public ClassBuff GetClone()
        {
            return (ClassBuff)this.MemberwiseClone();
        }
    }
}