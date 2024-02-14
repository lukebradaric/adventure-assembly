using AdventureAssembly.Units.Animation;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public abstract class CharacterData : SerializedScriptableObject
    {
        [BoxGroup("General")]
        [Tooltip("The base prefab for spawning this enemy. This should likely never change.")]
        [OdinSerialize] public Character Prefab { get; private set; } = default;

        [BoxGroup("General")]
        [OdinSerialize] public string Name { get; private set; }

        [BoxGroup("General")]
        [PreviewField(128, ObjectFieldAlignment.Left)]
        [OdinSerialize] public Sprite Sprite { get; private set; }

        [BoxGroup("General")]
        [Tooltip("What sprite should be used for this characters shadow?")]
        [OdinSerialize] public Sprite ShadowSprite { get; private set; } = default;

        [BoxGroup("General")]
        [Tooltip("What should the offset of the shadow be relative to the character?")]
        [OdinSerialize] public Vector2 ShadowOffset { get; private set; } = new Vector2(0, -0.225f);

        [BoxGroup("Animation")]
        [Tooltip("What animation should play when this unit moves?")]
        [OdinSerialize] public UnitTween MovementTween { get; private set; } = new DefaultUnitMovementTween();

        [BoxGroup("Audio")]
        [Tooltip("What sound should play when this enemy is hurt?")]
        [OdinSerialize] public ScriptableSound HurtSound { get; private set; } = default;

        [BoxGroup("Stats")]
        [Tooltip("What is the max health of this enemy?")]
        [OdinSerialize] public int MaxHealth { get; private set; }

        [BoxGroup("Components")]
        [Tooltip("What modifiers should be applied to this unit when it spawns?")]
        [OdinSerialize] public List<CharacterModifier> Modifiers { get; private set; } = new List<CharacterModifier>();
    }
}