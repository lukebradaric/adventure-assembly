using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Space]
    [Header("Settings")]
    [SerializeField] private AnimationCurve _enemySpawnCurve;
    [SerializeField] private float _enemySpawnRadius;

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

    }
}
