using AdventureAssembly.Core;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Items
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ItemData")]
    public class ItemData : SerializedScriptableObject
    {
        [BoxGroup("General")]
        [OdinSerialize] public string Name { get; protected set; }

        [BoxGroup("General")]
        [MultiLineProperty(8)]
        [OdinSerialize] public string Description { get; protected set; }

        [BoxGroup("General")]
        [PreviewField(128, ObjectFieldAlignment.Left)]
        [OdinSerialize] public Sprite Sprite { get; protected set; }

        [BoxGroup("General")]
        [OdinSerialize] public Color TooltipColor { get; protected set; } = Color.white;

        [BoxGroup("Settings")]
        [OdinSerialize] public Vector2 PriceRange { get; protected set; }

        [BoxGroup("Modifiers")]
        [OdinSerialize] public List<GlobalCharacterStatModifier> Modifiers { get; protected set; } = new List<GlobalCharacterStatModifier>();

        [BoxGroup("Tools")]
        [Button]
        private void Add()
        {
            ItemManager.Instance.Add(this);
        }

        [BoxGroup("Tools")]
        [Button]
        private void Remove()
        {
            ItemManager.Instance.RemoveByName(this);
        }
    }
}