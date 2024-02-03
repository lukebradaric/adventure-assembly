using AdventureAssembly.Units.Animation;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public abstract class CharacterUnitData : SerializedScriptableObject
    {
        [PropertySpace]
        [Title("Unit")]
        [OdinSerialize] public string Name { get; private set; }

        [PreviewField(128, ObjectFieldAlignment.Left)]
        [OdinSerialize] public Sprite Sprite { get; private set; }

        [OdinSerialize] public int MaxHealth { get; private set; }

        [Tooltip("What animation should play when this unit moves?")]
        [OdinSerialize] public UnitTween MovementTween { get; private set; } = new DefaultUnitMovementTween();

        [Tooltip("What modifiers should be applied to this unit when it spawns?")]
        [OdinSerialize] public List<CharacterUnitModifier> Modifiers { get; private set; } = new List<CharacterUnitModifier>();
    }
}