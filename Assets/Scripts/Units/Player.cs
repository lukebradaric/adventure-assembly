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
        [Header("Settings")]
        [SerializeField] private bool _updateIndicatorEveryFrame = false;

        [Space]
        [Header("Components")]
        [SerializeField] private Transform _directionIndicator;
        [SerializeField] private LineRenderer _lineRenderer;

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
            _lineRenderer.positionCount = _heroTransforms.Count + 1;
            Vector3 startPos = transform.position;
            startPos.y -= 0.4f;
            _lineRenderer.SetPosition(0, startPos);
            int count = 1;
            foreach (Transform trans in _heroTransforms)
            {
                Vector3 pos = trans.position;
                pos.y -= 0.4f;
                _lineRenderer.SetPosition(count, pos);
                count++;
            }

            if (_updateIndicatorEveryFrame)
            {
                _directionIndicator.transform.up = InputManager.Instance.MovementDirection.ToVector2();
            }
        }

        private void OnTurnUpdate()
        {
            if (!_updateIndicatorEveryFrame)
            {
                _directionIndicator.transform.up = InputManager.Instance.MovementDirection.ToVector2();
            }

            Vector2 startPosition = transform.position;
            Vector2 newPosition = transform.position + (Vector3)InputManager.Instance.MovementDirection.ToVector2();
            transform.DOMove(newPosition, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);

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