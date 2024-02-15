using AdventureAssembly.Units.Items;
using DG.Tweening;
using System.Collections.Generic;
using TinyTools.ScriptableEvents;
using TinyTools.ScriptableVariables;
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
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        [SerializeField] private FloatScriptableVariable _fadeTweenDuration;

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

        public void OnInterfaceOpened(GameEventData gameEventData)
        {
            Show();
        }

        public void OnInterfaceClosed(GameEventData gameEventData)
        {
            Hide();
        }

        public void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1f, _fadeTweenDuration.Value).SetUpdate(true);
        }

        public void Hide()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0f, _fadeTweenDuration.Value).SetUpdate(true);
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