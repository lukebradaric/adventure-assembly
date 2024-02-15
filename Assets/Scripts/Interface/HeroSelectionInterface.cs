using AdventureAssembly.Core;
using AdventureAssembly.Units.Heroes;
using DG.Tweening;
using System.Collections.Generic;
using TinyTools.Generics;
using TinyTools.ScriptableEvents;
using TinyTools.ScriptableVariables;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    /// <summary>
    /// Interface for handling new hero selections.
    /// </summary>
    public class HeroSelectionInterface : Singleton<HeroSelectionInterface>
    {
        [Space]
        [Header("Events")]
        [SerializeField] private GameScriptableEvent _onHeroSelected;

        [Space]
        [Header("Prefabs")]
        [SerializeField] private HeroSelectionElement _heroSelectionElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private ClassBuffInterface _classBuffInterface;
        [SerializeField] private HeroDataListScriptableVariable _heroDataList;
        [SerializeField] private RectTransform _horizontalLayoutTransform;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _backgroundImage;

        [Space]
        [Header("Settings")]
        [SerializeField] private FloatScriptableVariable _fadeTweenDuration;
        [SerializeField] private float _heroSelectionTweenPositionOffset = 1500;
        [SerializeField] private float _heroSelectionTweenDuration = 0.5f;
        [SerializeField] private float _heroSelectionTweenDurationOffset = 0.1f;
        [SerializeField] private Ease _tweenEase = Ease.OutBack;

        private List<HeroSelectionElement> _heroSelectionElements = new List<HeroSelectionElement>();

        public void OnChestOpened(GameEventData gameEventData)
        {
            Show(_heroDataList.GetRandom(3));
        }

        public void Show(List<HeroData> heroData)
        {
            TimeManager.Pause();

            _classBuffInterface.Show();

            ShowSelections(heroData);

            _canvasGroup.DOFade(1f, _fadeTweenDuration.Value).SetUpdate(true);
            _backgroundImage.DOFade(0.95f, _fadeTweenDuration.Value).SetUpdate(true);
        }

        public void Hide()
        {
            _classBuffInterface.Hide();

            _backgroundImage.DOFade(0f, _fadeTweenDuration.Value).SetUpdate(true);
            _canvasGroup.DOFade(0f, _fadeTweenDuration.Value).SetUpdate(true).OnComplete(() =>
            {
                foreach (HeroSelectionElement element in _heroSelectionElements)
                {
                    Destroy(element.gameObject);
                }

                _heroSelectionElements.Clear();

                TimeManager.Unpause();
            });
        }

        public void ShowSelections(List<HeroData> heroData)
        {
            // Instantiate hero selection prefabs in horizontal layout
            foreach (HeroData hero in heroData)
            {
                HeroSelectionElement heroSelectionElement = Instantiate(_heroSelectionElementPrefab, _horizontalLayoutTransform);
                heroSelectionElement.HeroData = hero;
                heroSelectionElement.HeroSelectionInterface = this;
                _heroSelectionElements.Add(heroSelectionElement);
            }

            // Force update horizontal layout
            LayoutRebuilder.ForceRebuildLayoutImmediate(_horizontalLayoutTransform);
            _horizontalLayoutGroup.enabled = false;

            // Tween each selection element into view
            float tweenDuration = _heroSelectionTweenDuration;
            foreach (HeroSelectionElement element in _heroSelectionElements)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(element.VerticalLayoutTransform);

                Vector3 position = element.transform.position;
                element.transform.position = new Vector3(position.x, _heroSelectionTweenPositionOffset, position.z);

                element.transform.DOMove(position, _heroSelectionTweenDuration + tweenDuration).SetUpdate(true).SetEase(_tweenEase);
                tweenDuration += _heroSelectionTweenDurationOffset;
            }

            // Enable and rebuild horizontal layout after tweens
            _horizontalLayoutGroup.transform.DOMove(Vector3.zero, tweenDuration).SetUpdate(true).SetRelative(true).OnComplete(() =>
            {
                _horizontalLayoutGroup.enabled = true;
                LayoutRebuilder.ForceRebuildLayoutImmediate(_horizontalLayoutTransform);
            });
        }

        public void OnHeroSelected(HeroData heroData)
        {
            // Disable interacting with all other selection elements
            foreach (HeroSelectionElement element in _heroSelectionElements)
            {
                element.Interactable = false;
            }

            ((HeroManager)HeroManager.Instance).AddHeroToSnake(heroData);

            _onHeroSelected.Invoke(this, heroData);

            Hide();
        }
    }
}