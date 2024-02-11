using DG.Tweening;
using TMPro;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    /// <summary>
    /// The damage text element that is spawned whenever enemies take damage.
    /// </summary>
    public class DamageTextElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _backgroundText;

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
        /// Fades the text over time.
        /// </summary>
        /// <param name="alpha">The alpha value to fade to</param>
        /// <param name="duration">The duration of the fade</param>
        /// <returns>Fade tween</returns>
        public Tween DOFade(float alpha, float duration, float backgroundDuration)
        {
            Tween textTween = _text.DOFade(alpha, duration);
            Tween backgroundTextTween = _backgroundText.DOFade(alpha, backgroundDuration);

            // Return whichever tween will finish last
            if(duration > backgroundDuration)
            {
                return textTween;
            }
            
            return backgroundTextTween;
        }
    }
}