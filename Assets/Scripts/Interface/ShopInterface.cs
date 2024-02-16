using AdventureAssembly.Core;
using AdventureAssembly.Units.Interactables;
using AdventureAssembly.Units.Items;
using DG.Tweening;
using TinyTools.Extensions;
using TinyTools.ScriptableEvents;
using TinyTools.ScriptableVariables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class ShopInterface : SelectionInterface
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private ShopElement _shopElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private TextMeshProUGUI _rerollPriceText;

        [Space]
        [Header("Settings")]
        [SerializeField] private FloatScriptableVariable _fadeTweenDuration;

        private ShopData _currentShopData;
        private int _rerollPrice;

        public void OnShopOpened(GameEventData gameEventData)
        {
            Show((ShopData)gameEventData.Data);
        }

        public void OnRerollShop()
        {
            // Check if player has enough gold to reroll
            if (GoldManager.Instance.TrySpend(_rerollPrice))
            {
                _rerollPrice *= 2;
                _rerollPriceText.text = _rerollPrice.ToString();
            }
            else
            {
                return;
            }

            foreach (ShopElement shopElement in _selectionElements)
            {
                // Don't reroll shop items that have already been purchased
                if (shopElement.Interactable == false)
                {
                    continue;
                }

                shopElement.ItemData = _currentShopData.ItemData.Random();
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_horizontalLayoutGroup.transform);
        }

        /// <summary>
        /// Shows a list of items in the interface.
        /// </summary>
        /// <param name="itemData"></param>
        public void Show(ShopData shopData)
        {
            this.OnShow();

            GameplayInterface.Instance.Show();

            _canvasGroup.DOFade(1f, _fadeTweenDuration.Value).SetUpdate(true);

            _currentShopData = shopData;
            _rerollPrice = _currentShopData.GetRerollPrice();
            _rerollPriceText.text = _rerollPrice.ToString();

            foreach (ItemData item in _currentShopData.GetRandom(3))
            {
                ShopElement shopElement = Instantiate(_shopElementPrefab, _horizontalLayoutGroup.transform);
                shopElement.ItemData = item;
                base.OnAddElement(shopElement);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_horizontalLayoutGroup.transform);
        }

        public void Hide()
        {
            this.OnHide();

            GameplayInterface.Instance.Hide();

            _canvasGroup.DOFade(0f, _fadeTweenDuration.Value).SetUpdate(true).OnComplete(() =>
            {
                DestroyAllSelectionElements();
            });

            _currentShopData = null;
        }

        public override void OnElementSelected(SelectionElement selectionElement)
        {
            base.OnElementSelected(selectionElement);

            ShopElement shopElement = (ShopElement)selectionElement;

            // If the player doesn't have enough gold to buy the item, return 
            if (!GoldManager.Instance.TrySpend(shopElement.Price))
            {
                return;
            }

            shopElement.OnPurchased();
            ItemManager.Instance.Add(shopElement.ItemData);
        }
    }
}