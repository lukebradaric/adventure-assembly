using AdventureAssembly.Core;
using AdventureAssembly.Units.Items;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.Extensions;
using UnityEngine;

namespace AdventureAssembly.Units.Interactables
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ShopData")]
    public class ShopData : SerializedScriptableObject
    {
        [OdinSerialize] public string Name { get; protected set; }

        [OdinSerialize] public Sprite Sprite { get; protected set; }

        [OdinSerialize] public Vector2 RerollPriceRange { get; protected set; }

        [OdinSerialize] public List<ItemData> ItemData { get; protected set; } = new List<ItemData>();

        public int GetRerollPrice()
        {
            return Random.Range((int)RerollPriceRange.x, (int)RerollPriceRange.y);
        }

        public List<ItemData> GetRandom(int count)
        {
            List<ItemData> temp = new List<ItemData>(ItemData);
            List<ItemData> items = new List<ItemData>();

            for (int i = 0; i < count; i++)
            {
                items.Add(temp.RemoveRandom());
            }

            return items;
        }
    }
}