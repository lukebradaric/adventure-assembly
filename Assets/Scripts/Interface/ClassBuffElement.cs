using AdventureAssembly.Units.Classes;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class ClassBuffElement : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Image _image;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _progressionText;
        [SerializeField] private Tooltip _descriptionTooltip;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _tweenDuration;
        [SerializeField] private float _startOffset;

        private Vector3 _backgroundImageStartPosition;

        private ClassData _classData = null;
        public ClassData ClassData
        {
            get
            {
                return _classData;
            }
            set
            {
                _classData = value;

                _image.color = _classData.Color;
                _backgroundImage.color = new Color(_classData.Color.r * 0.75f, _classData.Color.g * 0.75f, _classData.Color.b * 0.75f);
                _countText.text = "1";
                _nameText.text = _classData.Name;
                _descriptionTooltip.Text = _classData.Description;
                _descriptionTooltip.Color = _classData.Color;
            }
        }

        private void Awake()
        {
            _backgroundImageStartPosition = _backgroundImage.rectTransform.localPosition;
        }

        public void SetCountText(string count)
        {
            _countText.text = count;
        }

        public void Show()
        {
            _progressionText.enabled = true;
            _nameText.enabled = true;

            _backgroundImage.rectTransform.DOLocalMove(_backgroundImageStartPosition, _tweenDuration).SetUpdate(true);
        }

        public void Hide()
        {
            Vector3 position = _backgroundImageStartPosition;
            position.x = _startOffset;

            _backgroundImage.rectTransform.DOLocalMove(position, _tweenDuration).SetUpdate(true);

            _progressionText.enabled = false;
            _nameText.enabled = false;
        }
    }
}