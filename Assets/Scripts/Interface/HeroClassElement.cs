using AdventureAssembly.Units.Heroes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class HeroClassElement : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _text;

        private ClassData _classData;
        public ClassData ClassData
        {
            get
            {
                return _classData;
            }
            set
            {
                _classData = value;

                _backgroundImage.color = _classData.Color;
                _text.text = _classData.Name;
            }
        }
    }
}