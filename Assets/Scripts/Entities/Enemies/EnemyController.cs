using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private Enemy _enemy;

    [Space]
    [Header("Settings")]
    [SerializeField] protected int _attackDamage;

    [PropertySpace]
    [Title("Animation")]
    [SerializeReference]
    [SerializeField] private EntityTween _attackTween = new MeleeAttackEntityTween();

    private void OnValidate()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        TurnManager.LateTurnUpdate += OnTurnUpdate;
    }

    private void OnDisable()
    {
        TurnManager.LateTurnUpdate -= OnTurnUpdate;
    }

    protected virtual void OnTurnUpdate()
    {
        if (CharacterManager.TryGet(_enemy.Position.GetAdjacents(), out Character character))
        {
            // TODO: Play attack animation
            _attackTween.Animate(_enemy, character.Position, TurnManager.TurnInterval);
            character.Damage(_attackDamage);
            return;
        }

        _enemy.Move(GetMovementTowardsPlayer());
    }

    protected virtual Vector2Int GetMovementTowardsPlayer()
    {
        List<Vector2Int> positions = _enemy.Position.GetAdjacents();

        Vector2Int nearestPosition = Vector2Int.zero;
        float nearestDistance = float.MaxValue;
        Vector2 playerCenterPosition = CharacterManager.GetNearest(_enemy.Position).Position;
        foreach (Vector2Int position in positions)
        {
            float distance = Vector2.Distance(playerCenterPosition, position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPosition = position;
            }
        }

        // If nearest position is already taken, return no movement
        if (!EnemyManager.RequestPosition(nearestPosition))
        {
            return Vector2Int.zero;
        }

        return nearestPosition - _enemy.Position;
    }
}