using AdventureAssembly.Units.Items;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class ItemElement : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Image _image;
        [SerializeField] private Tooltip _tooltip;

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
                _tooltip.Text = $"{_itemData.Name}\n\n{_itemData.Description}";
                _tooltip.Color = _itemData.TooltipColor;
                _tooltip.Pivot = new Vector2(1, 1);
            }
        }
    }
}