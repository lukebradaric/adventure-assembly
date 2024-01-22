using AdventureAssembly.Core;
using AdventureAssembly.Input;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Player : SerializedMonoBehaviour
    {
        [PropertySpace]
        [Title("Debug")]
        [OdinSerialize] public List<Unit> Units { get; private set; } = new List<Unit>();

        private void OnEnable()
        {
            TurnManager.TurnUpdate += OnTurnUpdate;
        }

        private void OnDisable()
        {
            TurnManager.TurnUpdate -= OnTurnUpdate;
        }

        private void OnTurnUpdate()
        {
            UpdateUnitPositions();
        }

        private void UpdateUnitPositions()
        {
            // Calculate start position of party head
            Vector2 startPosition = transform.position;
            Vector2 newPosition = transform.position + (Vector3)InputManager.Instance.MovementVector;
            transform.DOMove(newPosition, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);

            // Move each unit in the party
            Vector2 previousPosition = startPosition;
            foreach (Unit unit in Units)
            {
                Vector2 startPos = unit.transform.position;
                Vector2 newPos = new Vector2(previousPosition.x - unit.transform.position.x, previousPosition.y - unit.transform.position.y);
                unit.transform.DOMove(previousPosition, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);
                previousPosition = startPos;
            }
        }
    }
}