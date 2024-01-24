using AdventureAssembly.Units.Animation;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public abstract class UnitData : SerializedScriptableObject
    {
        [PropertySpace]
        [OdinSerialize] public string Name { get; private set; }

        [PreviewField(128, ObjectFieldAlignment.Left)]
        [OdinSerialize] public Sprite Sprite { get; private set; }

        [OdinSerialize] public UnitMovementTween MovementTween { get; private set; } = new DefaultUnitMovementTween();
    }
}