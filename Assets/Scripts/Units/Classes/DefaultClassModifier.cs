using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Classes
{
    public class DefaultClassModifier : ClassModifier
    {
        [BoxGroup("Settings")]
        [Tooltip("Do you also want the option to add modifiers to enemies using this class?")]
        [SerializeField] private bool _enableEnemyModifiers = false;

        [BoxGroup("Hero Modifiers")]
        [SerializeField] private List<CharacterUnitModifier> _heroModifiers = new List<CharacterUnitModifier>();

        [BoxGroup("Enemy Modifiers")]
        [ShowIf(nameof(_enableEnemyModifiers))]
        [SerializeField] private List<CharacterUnitModifier> _enemyModifiers = new List<CharacterUnitModifier>();

        public override void Apply()
        {
            HeroManager.AddGlobalModifiers(_heroModifiers);

            if (_enableEnemyModifiers)
            {
                EnemyManager.AddGlobalModifiers(_enemyModifiers);
            }
        }

        public override void Remove()
        {
            HeroManager.RemoveGlobalModifiers(_heroModifiers);

            if (_enableEnemyModifiers)
            {
                EnemyManager.RemoveGlobalModifiers(_enemyModifiers);
            }
        }
    }
}