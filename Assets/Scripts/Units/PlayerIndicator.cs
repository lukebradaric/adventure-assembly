using AdventureAssembly.Core;
using AdventureAssembly.Core.Extensions;
using AdventureAssembly.Input;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class PlayerIndicator : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _indicatorTransform;
        [SerializeField] private Transform _indicatorSpriteTransform;
        [SerializeField] private LineRenderer _indicatorLineRenderer;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _indicatorTweenDuration = 0.1f;
        [SerializeField] private Vector2 _indicatorOffset = new Vector2(0.9f, 0.4f);

        private void OnEnable()
        {
            TurnManager.TurnLateUpdate += OnTurnLateUpdate;
        }

        private void OnDisable()
        {
            TurnManager.TurnLateUpdate -= OnTurnLateUpdate;
        }

        private void Update()
        {
            UpdateIndicatorLineRenderer();
        }

        private void OnTurnLateUpdate()
        {
            UpdateIndicator();
        }

        private void UpdateIndicator()
        {
            Vector2 indicatorDirection = _player.LastMovementVector;

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
            _indicatorLineRenderer.positionCount = _player.Heroes.Count + 1;
            _indicatorLineRenderer.SetPosition(0, _indicatorSpriteTransform.position);

            int count = 1;
            foreach (Hero hero in _player.Heroes)
            {
                Vector3 pos = hero.transform.position;
                pos.y -= 0.4f;
                _indicatorLineRenderer.SetPosition(count, pos);
                count++;
            }
        }
    }
}