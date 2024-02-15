using AdventureAssembly.Units.Modifiers;
using System.Collections.Generic;
using TinyTools.Extensions;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Items
{
    /// <summary>
    /// Manages adding/removing items, and keeping track of what items the player current has.
    /// </summary>
    public class ItemManager : Singleton<ItemManager>
    {
        /// <summary>
        /// List of items the player currently has equipped.
        /// </summary>
        public List<ItemData> Items { get; protected set; } = new List<ItemData>();

        // Dictionary of the modifiers applied from each item the player has
        private Dictionary<ItemData, List<GlobalCharacterStatModifier>> _itemModifiers = new Dictionary<ItemData, List<GlobalCharacterStatModifier>>();

        /// <summary>
        /// Adds an Item to the players list of items and applies all modifiers.
        /// </summary>
        /// <param name="itemData">The item to add</param>
        public void Add(ItemData itemData)
        {
            // Create clone of itemdata
            ItemData newItem = itemData.GetClone();

            // Clone each modifier on the item and add it to a list
            List<GlobalCharacterStatModifier> modifiers = new List<GlobalCharacterStatModifier>();
            foreach (GlobalCharacterStatModifier modifier in newItem.Modifiers)
            {
                GlobalCharacterStatModifier newModifier = (GlobalCharacterStatModifier)modifier.GetClone();
                modifiers.Add(newModifier);
                newModifier.Apply();
            }


            // Add item to list and dictionary
            Items.Add(newItem);
            _itemModifiers.Add(newItem, modifiers);
        }

        /// <summary>
        /// Removes an item from the players list of items and removes all modifiers.
        /// </summary>
        /// <param name="itemData">The item to remove</param>
        public void Remove(ItemData itemData)
        {
            if (!_itemModifiers.ContainsKey(itemData))
            {
                Debug.LogError($"Could not find {itemData.name} to remove!");
                return;
            }

            // Remove all modifiers from the item
            foreach (GlobalCharacterStatModifier modifier in _itemModifiers[itemData])
            {
                modifier.Remove();
            }

            // Remove the entry from the dictionary
            Items.Remove(itemData);
            _itemModifiers.Remove(itemData);
        }

        /// <summary>
        /// Removes an item with a matching name.
        /// </summary>
        /// <param name="itemData">The item with the name to remove</param>
        public void RemoveByName(ItemData itemData)
        {
            foreach (ItemData item in Items)
            {
                if (item.Name == itemData.Name)
                {
                    Remove(item);
                    return;
                }
            }

            Debug.LogError($"Could not find {itemData.Name} to remove!");
        }
    }
}