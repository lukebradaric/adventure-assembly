using AdventureAssembly.Units.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class ShopElement : SelectionElement
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _priceText;

        public int Price { get; private set; }

        private ItemData _itemData;
        public ItemData ItemData
        {
            get
            {
                return _itemData;
            }
            set
            {
                _itemData = value;

                _image.sprite = _itemData.Sprite;
                _nameText.text = _itemData.Name;
                _descriptionText.text = _itemData.Description;
                Price = _itemData.GetPrice();
                _priceText.text = Price.ToString();
            }
        }

        public void OnPurchased()
        {
            this.Interactable = false;
            _canvasGroup.alpha = 0.1f;
        }
    }
}