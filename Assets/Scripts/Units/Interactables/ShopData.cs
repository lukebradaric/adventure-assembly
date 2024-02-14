using AdventureAssembly.Core;
using AdventureAssembly.Units.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Interactables
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ShopData")]
    public class ShopData : SerializedScriptableObject
    {
        [OdinSerialize] public string Name { get; protected set; }

        [OdinSerialize] public Sprite Sprite { get; protected set; }

        [OdinSerialize] public List<ItemData> ItemData { get; protected set; } = new List<ItemData>();
    }
}