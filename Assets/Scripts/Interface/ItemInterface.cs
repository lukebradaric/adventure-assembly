using AdventureAssembly.Units.Items;
using System.Collections.Generic;
using TinyTools.ScriptableEvents;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class ItemInterface : MonoBehaviour
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private ItemElement _itemElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        // The item element associated with each item data
        private Dictionary<ItemData, ItemElement> _itemElements = new Dictionary<ItemData, ItemElement>();

        public void OnItemAdded(GameEventData gameEventData)
        {
            AddItemElement((ItemData)gameEventData.Data);
        }

        public void OnItemRemoved(GameEventData gameEventData)
        {
            RemoveItemElement((ItemData)gameEventData.Data);
        }

        public void AddItemElement(ItemData itemData)
        {
            ItemElement itemElement = Instantiate(_itemElementPrefab, _gridLayoutGroup.transform);
            itemElement.ItemData = itemData;

            _itemElements.Add(itemData, itemElement);
        }

        public void RemoveItemElement(ItemData itemData)
        {
            if (!_itemElements.ContainsKey(itemData))
            {
                Debug.LogError("Could not find ItemData to remove from interface!");
                return;
            }

            // Destroy and remove element from interface
            Destroy(_itemElements[itemData].gameObject);
            _itemElements.Remove(itemData);
        }
    }
}