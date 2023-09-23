using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [Space]
    [Header("Settings")]
    [SerializeField] private int _turnDuration = 1;
    public int TurnDuration => _turnDuration;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _damageRadius = 1;

    private int _turnsRemaining;

    private void Awake()
    {
        _turnsRemaining = _turnDuration;
    }

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
        _turnsRemaining--;

        DamageArea();

        if (_turnsRemaining <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DamageArea()
    {
        foreach (Enemy enemy in EnemyManager.GetInRadius(transform.position, _damageRadius))
        {
            enemy.Damage(_damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }
}
