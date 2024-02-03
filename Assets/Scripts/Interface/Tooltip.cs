using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventureAssembly.Interface
{
    public class Tooltip : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Space]
        [Header("Components")]
        [SerializeField] private TooltipElement _tooltipElementPrefab = default;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _tweenDuration = 0.25f;
        [Tooltip("Should this tooltip automatically adjust itself to ensure it is on screen?")]
        [SerializeField] private bool _smartPosition = true;

        [PropertySpace]
        [Title("Debugging")]
        [OdinSerialize] public string Text { get; set; }
        [OdinSerialize] public Color Color { get; set; } = Color.white;

        private TooltipElement _tooltipElement = null;
        private bool _hovering = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_tooltipElement == null)
            {
                _tooltipElement = Instantiate(_tooltipElementPrefab, InterfaceManager.Instance.transform);
                _tooltipElement.Text.text = Text;
                _tooltipElement.Image.color = Color;
            }

            _hovering = true;
            _tooltipElement.CanvasGroup.DOFade(1f, _tweenDuration).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovering = false;
            _tooltipElement.CanvasGroup.DOFade(0f, _tweenDuration).SetUpdate(true);
        }

        // If this object is destroyed, destroy the created tooltip element
        private void OnDestroy()
        {
            if (_tooltipElement == null)
            {
                return;
            }

            Destroy(_tooltipElement.gameObject);
        }

        private void Update()
        {
            if (!_hovering)
            {
                return;
            }

            Vector3 position = Input.mousePosition;

            // If smart positioning is enabled and tooltip is off screen, offset to ensure it is on screen
            if (_smartPosition && position.y - _tooltipElement.RectTransform.sizeDelta.y < 0)
            {
                position.y += Mathf.Abs(position.y - _tooltipElement.RectTransform.sizeDelta.y);
            }

            _tooltipElement.transform.position = position;
        }
    }
}