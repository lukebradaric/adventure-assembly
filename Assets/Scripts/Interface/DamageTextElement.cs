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
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI underlayText;

        /// <summary>
        /// The text value of this element.
        /// </summary>
        public string Text
        {
            get
            {
                return text.text;
            }
            set
            {
                text.text = value;
                underlayText.text = value;
            }
        }

        /// <summary>
        /// The color of the top level text.
        /// </summary>
        public Color Color
        {
            get
            {
                return text.color;
            }
            set
            {
                text.color = value;
            }
        }

        /// <summary>
        /// Fades the text over time.
        /// </summary>
        /// <param name="alpha">The alpha value to fade to</param>
        /// <param name="duration">The duration of the fade</param>
        /// <returns>Fade tween</returns>
        public Tween DOFade(float alpha, float duration)
        {
            text.DOFade(alpha, duration);
            return underlayText.DOFade(alpha, duration * 1.1f);
        }
    }
}