using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    /// <summary>
    /// The damage text element that is spawned whenever enemies take damage.
    /// </summary>
    public class DamageTextElement : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _backgroundText;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Space]
        [Header("Settings")]
        [SerializeField] private bool _tweenCanvasGroup = false;
        [SerializeField] private float _tweenDuration = 1f;
        [HideIf(nameof(_tweenCanvasGroup))]
        [SerializeField] private float _backgroundTweenDuration = 1f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [HideIf(nameof(_tweenCanvasGroup))]
        [SerializeField] private Ease _backgroundEase = Ease.Linear;

        /// <summary>
        /// The text value of this element.
        /// </summary>
        public string Text
        {
            get
            {
                return _text.text;
            }
            set
            {
                _text.text = value;
                _backgroundText.text = value;
            }
        }

        /// <summary>
        /// The color of the top level text.
        /// </summary>
        public Color Color
        {
            get
            {
                return _text.color;
            }
            set
            {
                _text.color = value;
            }
        }

        /// <summary>
        /// The background underlay color of the text.
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                return _backgroundText.color;
            }
            set
            {
                _backgroundText.color = value;
            }
        }

        /// <summary>
        /// Tweens the damage text to fade out over time.
        /// </summary>
        /// <returns></returns>
        public Tween DoFadeOutTween()
        {
            if (_tweenCanvasGroup)
            {
                return _canvasGroup.DOFade(0f, _tweenDuration).SetEase(_ease);
            }

            if (_tweenDuration > _backgroundTweenDuration)
            {
                _backgroundText.DOFade(0f, _backgroundTweenDuration).SetEase(_backgroundEase);
                return _text.DOFade(0f, _tweenDuration).SetEase(_ease);
            }

            _text.DOFade(0f, _tweenDuration).SetEase(_ease);
            return _backgroundText.DOFade(0, _backgroundTweenDuration).SetEase(_backgroundEase);
        }
    }
}