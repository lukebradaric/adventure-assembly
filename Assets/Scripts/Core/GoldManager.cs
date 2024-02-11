using DG.Tweening;
using System.Collections;
using TinyTools.Generics;
using TinyTools.ScriptableEvents;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Core
{
    /// <summary>
    /// Manager for adding and spending gold.
    /// </summary>
    public class GoldManager : Singleton<GoldManager>
    {
        [Space]
        [Header("Events")]
        [SerializeField] private GameScriptableEvent _onGoldCountChanged;

        [Space]
        [Header("Prefabs")]
        [Tooltip("What should be instantiated when spawning gold?")]
        [SerializeField] private GameObject _goldPrefab;

        [Space]
        [Header("Settings")]
        [Tooltip("What curve should the tween of the coin position follow? (InBack)")]
        [SerializeField] private AnimationCurve _tweenCurve;
        [Tooltip("How long should the tween take per world unit space? (Duration = Distance * TweenDistanceDuration)")]
        [SerializeField] private float _tweenDistanceDuration;
        [Tooltip("What is the lowest possible duration for the movement tween?")]
        [SerializeField] private float _minTweenDuration;
        [Tooltip("When adding more than once gold at once, how long should the delay between spawning be?")]
        [SerializeField] private float _delayPerGold;

        [Space]
        [Header("Audio")]
        [SerializeField] private ScriptableSound _addGoldSound;

        /// <summary>
        /// The amount of gold the player currently has.
        /// </summary>
        private int _goldCount;
        public int GoldCount
        {
            get
            {
                return _goldCount;
            }
            set
            {
                _goldCount = value;
                _onGoldCountChanged?.Invoke(this, _goldCount);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            GoldCount = 0;
        }

        /// <summary>
        /// Tries to spend an amount of gold.
        /// </summary>
        /// <param name="amount">The amount of gold to spend.</param>
        /// <returns>True if the gold was succesfully spent.
        /// False if the gold was not spent.</returns>
        public bool TrySpend(int amount)
        {
            if (GoldCount < amount)
            {
                return false;
            }

            GoldCount -= amount;
            return true;
        }

        /// <summary>
        /// Adds gold with an in-game tweening animation.
        /// </summary>
        /// <param name="position">The starting position to spawn the gold.</param>
        public void AddGold(Vector2 position)
        {
            GameObject goldObject = Instantiate(_goldPrefab, position, Quaternion.identity);
            Vector3 tweenPosition = Camera.main.ScreenToWorldPoint(new Vector2(0 + (Screen.width * 0.05f), Screen.height - (Screen.height * 0.05f)));
            float tweenDuration = Vector2.Distance(goldObject.transform.position, tweenPosition) * _tweenDistanceDuration;

            if (tweenDuration < _minTweenDuration)
            {
                tweenDuration = _minTweenDuration;
            }

            goldObject.transform.DOMove(tweenPosition, tweenDuration).SetEase(_tweenCurve).SetUpdate(true).OnComplete(() =>
            {
                GoldCount++;
                Destroy(goldObject);
            });

            _addGoldSound?.Play();
        }

        /// <summary>
        /// Adds multiple gold with an in-game tweening animation.
        /// </summary>
        /// <param name="position">The starting position to spawn the gold.</param>
        /// <param name="amount">The amount of gold to add.</param>
        public void AddGold(Vector2 position, int amount)
        {
            StartCoroutine(AddGoldCoroutine(position, amount));
        }

        private IEnumerator AddGoldCoroutine(Vector2 position, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddGold(position);
                yield return new WaitForSecondsRealtime(_delayPerGold);
            }
        }
    }
}