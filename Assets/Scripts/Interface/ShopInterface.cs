using AdventureAssembly.Units.Items;
using System.Collections.Generic;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class ShopInterface : Singleton<ShopInterface>
    {
        [Space]
        [Header("Components")]
        [SerializeField] private ShopElement _shopElementPrefab;

        /// <summary>
        /// Shows a list of items in the interface.
        /// </summary>
        /// <param name="itemData"></param>
        public void Show(List<ItemData> itemData)
        {

        }
    }
}