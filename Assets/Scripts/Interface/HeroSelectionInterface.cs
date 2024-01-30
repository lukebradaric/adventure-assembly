using AdventureAssembly.Core;
using AdventureAssembly.Units.Heroes;
using DG.Tweening;
using System.Collections.Generic;
using TinyTools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class HeroSelectionInterface : MonoBehaviour
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private HeroSelectionElement _heroSelectionElementPrefab;

        [Space]
        [Header("Components")]
        [SerializeField] private RectTransform _horizontalLayoutTransform;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        [SerializeField] private CanvasGroup _canvasGroup;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _fadeTweenDuration = 0.5f;
        [SerializeField] private float _heroSelectionTweenPositionOffset = 1500;
        [SerializeField] private float _heroSelectionTweenDuration = 0.5f;
        [SerializeField] private float _heroSelectionTweenDurationOffset = 0.1f;
        [SerializeField] private Ease _tweenEase = Ease.OutBack;

        [Space]
        [Header("Debug")]
        [SerializeField] private List<HeroData> _testHeroData = new List<HeroData>();

        private List<HeroSelectionElement> _heroSelectionElements = new List<HeroSelectionElement>();

        private bool _showing = false;

        public void Show(List<HeroData> heroData)
        {
            _showing = true;
            TimeManager.Pause();

            ShowSelections(heroData);

            _canvasGroup.DOFade(1f, _fadeTweenDuration).SetUpdate(true);
        }

        public void Hide()
        {
            _showing = false;

            _canvasGroup.DOFade(0f, _fadeTweenDuration).SetUpdate(true).OnComplete(() =>
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
            ((HeroManager)HeroManager.Instance).SpawnHero(heroData);
            Hide();
        }
    }
}