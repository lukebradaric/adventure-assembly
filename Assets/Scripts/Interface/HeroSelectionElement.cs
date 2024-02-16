using AdventureAssembly.Units.Classes;
using AdventureAssembly.Units.Heroes;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class HeroSelectionElement : SelectionElement
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private HeroClassElement _heroClassElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private RectTransform _verticalLayoutTransform;
        [SerializeField] private RectTransform _classVerticalLayoutTransform;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _abilitySpeedText;
        [SerializeField] private TextMeshProUGUI _maxHealthText;
        [SerializeField] private Image _heroImage;
        [SerializeField] private Image _shadowImage;
        [SerializeField] private Image _heroBackgroundImage;
        [SerializeField] private Image _hoverBackgroundImage;

        [Space]
        [Header("Settings")]
        [Tooltip("What should the alpha of the background image be when this is hovered?")]
        [SerializeField] private float _hoverBackgroundImageAlpha;

        public RectTransform VerticalLayoutTransform => _verticalLayoutTransform;

        private HeroData _heroData = null;
        public HeroData HeroData
        {
            get
            {
                return _heroData;
            }
            set
            {
                _heroData = value;

                _nameText.text = _heroData.Name;
                _descriptionText.text = _heroData.Description;
                _abilitySpeedText.text = _heroData.AbilitySpeed.ToString();
                _maxHealthText.text = _heroData.MaxHealth.ToString();

                _heroImage.sprite = _heroData.Sprite;
                //_shadowImage.sprite = _heroData.ShadowSprite;
                _heroBackgroundImage.color = _heroData.BackgroundColor;

                foreach (ClassData classData in _heroData.ClassData)
                {
                    HeroClassElement heroClassElement = Instantiate(_heroClassElementPrefab, _classVerticalLayoutTransform);
                    heroClassElement.ClassData = classData;
                }
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _hoverBackgroundImage.DOFade(_hoverBackgroundImageAlpha, this.TweenDuration).SetUpdate(true);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            _hoverBackgroundImage.DOFade(0f, TweenDuration).SetUpdate(true);
        }
    }
}