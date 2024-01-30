using AdventureAssembly.Units.Heroes;
using DG.Tweening;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class HeroSelectionElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private HeroClassElement _heroClassElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private CanvasGroup _canvasGroup;
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
        [Tooltip("What should the alpha of this element be when a different element is hovered?")]
        [SerializeField] private float _otherHoverCanvasGroupAlpha;
        [Tooltip("How long should the tween animation for hovering be?")]
        [SerializeField] private float _hoverTweenDuration;
        [Tooltip("How large should this element scale to when hovered?")]
        [SerializeField] private float _hoverScale;
        [Tooltip("What should the alpha of the background image be when this is hovered?")]
        [SerializeField] private float _hoverBackgroundImageAlpha;

        public RectTransform VerticalLayoutTransform => _verticalLayoutTransform;

        public HeroSelectionInterface HeroSelectionInterface { get; set; }

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
                // TODO: Set ability speed text to ability speed
                //_abilitySpeedText = _heroData.AbilitySpeed;
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_hoverScale, _hoverTweenDuration).SetUpdate(true);
            _hoverBackgroundImage.DOFade(_hoverBackgroundImageAlpha, _hoverTweenDuration).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, _hoverTweenDuration).SetUpdate(true);
            _hoverBackgroundImage.DOFade(0f, _hoverTweenDuration).SetUpdate(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HeroSelectionInterface.OnHeroSelected(_heroData);
        }
    }
}