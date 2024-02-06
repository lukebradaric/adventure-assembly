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

        [BoxGroup("Settings")]
        [Tooltip("Do you also want the option to add modifiers to enemies using this class?")]
        [SerializeField] private bool _enableEnemyModifiers = false;

        [BoxGroup("Class Settings")]
        [Tooltip("Do you want to apply this buff to only specific classes?")]
        [SerializeField] private bool _specificClasses;

        [BoxGroup("Class Settings")]
        [Tooltip("What classes should this buff be applied to?")]
        [ShowIf(nameof(_specificClasses))]
        [SerializeField] private List<ClassData> _classes = new List<ClassData>();

        [BoxGroup("Hero Modifiers")]
        [SerializeField] private List<CharacterModifier> _heroModifiers = new List<CharacterModifier>();

        [BoxGroup("Enemy Modifiers")]
        [ShowIf(nameof(_enableEnemyModifiers))]
        [SerializeField] private List<CharacterModifier> _enemyModifiers = new List<CharacterModifier>();

        public virtual void Apply()
        {
            HeroManager.AddGlobalModifiers(_heroModifiers);

            if (_enableEnemyModifiers)
            {
                EnemyManager.AddGlobalModifiers(_enemyModifiers);
            }
        }

        public virtual void Remove()
        {
            HeroManager.RemoveGlobalModifiers(_heroModifiers);

            if (_enableEnemyModifiers)
            {
                EnemyManager.RemoveGlobalModifiers(_enemyModifiers);
            }
        }

        public ClassBuff GetClone()
        {
            return (ClassBuff)this.MemberwiseClone();
        }
    }
}