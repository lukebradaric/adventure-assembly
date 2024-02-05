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
        [Tooltip("Do we want to apply a list of modifiers to heroes instead of a single one?")]
        [SerializeField] private bool _listOfModifiers = false;

        [BoxGroup("Modifiers")]
        [HideIf((nameof(_listOfModifiers)))]
        [SerializeField] private CharacterUnitModifier _characterUnitModifier = new SmartHeroModifier();

        [BoxGroup("Modifiers")]
        [ShowIf((nameof(_listOfModifiers)))]
        [SerializeField] private List<CharacterUnitModifier> _characterUnitModifiers = new List<CharacterUnitModifier>();

        public override void Apply()
        {
            if (_listOfModifiers)
            {
                HeroManager.AddGlobalModifiers(_characterUnitModifiers);
                return;
            }

            HeroManager.AddGlobalModifier(_characterUnitModifier);
        }

        public override void Remove()
        {
            if (_listOfModifiers)
            {
                HeroManager.RemoveGlobalModifiers(_characterUnitModifiers);
                return;
            }

            HeroManager.RemoveGlobalModifier(_characterUnitModifier);
        }
    }
}