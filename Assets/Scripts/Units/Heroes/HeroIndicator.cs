﻿using AdventureAssembly.Core;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroIndicator : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private HeroManager _heroManager;
        [SerializeField] private Transform _indicatorTransform;
        [SerializeField] private Transform _indicatorSpriteTransform;
        [SerializeField] private LineRenderer _indicatorLineRenderer;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _indicatorTweenDuration = 0.1f;
        [SerializeField] private Vector2 _indicatorOffset = new Vector2(0.9f, 0.4f);

        private void OnEnable()
        {
            TickManager.TickLateUpdate += OnTurnLateUpdate;
            TickManager.EnemyTickLateUpdate += OnTurnLateUpdate;
        }

        private void OnDisable()
        {
            TickManager.TickLateUpdate -= OnTurnLateUpdate;
            TickManager.EnemyTickLateUpdate -= OnTurnLateUpdate;
        }

        private void Start()
        {
            StartCoroutine(UpdateCoroutine());
        }

        private void Update()
        {
            UpdateIndicatorLineRenderer();
        }

        private IEnumerator UpdateCoroutine()
        {
            yield return new WaitForSeconds(_indicatorTweenDuration);
            UpdateIndicator();
            StartCoroutine(UpdateCoroutine());
        }

        private void OnTurnLateUpdate()
        {
            //UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            Vector2 indicatorDirection = _heroManager.NextMovementVector;

            // Calculate indicator rotation based on input direction
            float rad = Mathf.Atan2(indicatorDirection.y, indicatorDirection.x);
            float rotation = (rad * (180 / Mathf.PI)) - 90f;

            // Determine arrow sprite position (Position is offset when moving horizontally)
            Vector2 spritePosition = new Vector2(0, _indicatorOffset.x);
            if (indicatorDirection == Vector2.right)
                spritePosition.x = _indicatorOffset.y;
            else if (indicatorDirection == Vector2.left)
                spritePosition.x = -_indicatorOffset.y;

            // Tween indicator arrow position
            _indicatorSpriteTransform.DOLocalMove(spritePosition, _indicatorTweenDuration);

            // Tween indicator rotation
            _indicatorTransform.DORotate(new Vector3(0, 0, rotation), _indicatorTweenDuration).OnUpdate(() =>
            {
                UpdateIndicatorLineRenderer();
            });
        }

        private void UpdateIndicatorLineRenderer()
        {
            _indicatorLineRenderer.positionCount = HeroManager.Instance.Units.Count + 1;
            _indicatorLineRenderer.SetPosition(0, _indicatorSpriteTransform.position);

            int count = 1;
            foreach (Hero hero in HeroManager.Instance.Units)
            {
                Vector3 pos = hero.transform.position;
                pos.y -= 0.4f;
                _indicatorLineRenderer.SetPosition(count, pos);
                count++;
            }
        }
    }
}