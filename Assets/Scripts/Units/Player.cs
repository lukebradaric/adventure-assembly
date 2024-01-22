using AdventureAssembly.Core;
using AdventureAssembly.Core.Extensions;
using AdventureAssembly.Input;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Player : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Transform _directionIndicator;
        [SerializeField] private Transform _directionIndicatorSprite;
        [SerializeField] private LineRenderer _directionIndicatorLineRenderer;

        [Space]
        [Header("Settings")]
        [SerializeField] private bool _updateIndicatorEveryFrame = false;
        [SerializeField] private float _directionIndicatorTweenDuration = 0.15f;

        [Space]
        [Header("Testing")]
        [SerializeField] private List<Transform> _heroTransforms = new List<Transform>();

        private void OnEnable()
        {
            TurnManager.TurnUpdate += OnTurnUpdate;
        }

        private void OnDisable()
        {
            TurnManager.TurnUpdate -= OnTurnUpdate;
        }

        private void Update()
        {
            if (_updateIndicatorEveryFrame)
            {
                UpdateDirectionIndicator();
            }

            UpdateDirectionIndicatorLineRenderer();
        }

        private void OnTurnUpdate()
        {
            if (!_updateIndicatorEveryFrame)
            {
                UpdateDirectionIndicator();
            }

            UpdateUnitPositions();
        }

        private void UpdateDirectionIndicatorLineRenderer()
        {
            _directionIndicatorLineRenderer.positionCount = _heroTransforms.Count + 2;

            _directionIndicatorLineRenderer.SetPosition(0, _directionIndicatorSprite.position);

            Vector3 startPos = transform.position;
            startPos.y -= 0.4f;
            _directionIndicatorLineRenderer.SetPosition(1, startPos);

            int count = 2;
            foreach (Transform trans in _heroTransforms)
            {
                Vector3 pos = trans.position;
                pos.y -= 0.4f;
                _directionIndicatorLineRenderer.SetPosition(count, pos);
                count++;
            }
        }

        private void UpdateDirectionIndicator()
        {
            Vector2 indicatorDirection = InputManager.Instance.MovementDirection.ToVector2();

            // Calculate indicator rotation based on input direction
            float rad = Mathf.Atan2(indicatorDirection.y, indicatorDirection.x);
            float rotation = (rad * (180 / Mathf.PI)) - 90f;

            // Determine arrow sprite position (Position is offset when moving horizontally)
            Vector2 spritePosition = new Vector2(0, 1);
            if (indicatorDirection == Vector2.right)
            {
                spritePosition.x = 0.4f;
            }
            else if (indicatorDirection == Vector2.left)
            {
                spritePosition.x = -0.4f;
            }

            // Tween indicator arrow position
            _directionIndicatorSprite.DOLocalMove(spritePosition, 0.15f);

            // Tween indicator rotation
            _directionIndicator.DORotate(new Vector3(0, 0, rotation), 0.15f).OnUpdate(() =>
            {
                UpdateDirectionIndicatorLineRenderer();
            });

            // Update line renderer to reflect indicator rotation
            UpdateDirectionIndicatorLineRenderer();
        }

        private void UpdateUnitPositions()
        {
            // Calculate start position of party head
            Vector2 startPosition = transform.position;
            Vector2 newPosition = transform.position + (Vector3)InputManager.Instance.MovementDirection.ToVector2();
            transform.DOMove(newPosition, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);

            // Move each unit in the party
            Vector2 previousPosition = startPosition;
            foreach (Transform trans in _heroTransforms)
            {
                Vector2 startPos = trans.position;
                Vector2 newPos = new Vector2(previousPosition.x - trans.position.x, previousPosition.y - trans.position.y);
                trans.DOMove(previousPosition, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);
                previousPosition = startPos;
            }
        }
    }
}