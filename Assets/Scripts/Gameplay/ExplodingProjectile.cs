using UnityEngine;

public class ExplodingProjectile : Projectile
{
    [Space]
    [Header("Settings")]
    [SerializeField] private int _explosionDamage;
    [SerializeField] private float _explosionRadius;

    protected override void OnEnemyCollision(Enemy enemy)
    {
        foreach (var e in EnemyManager.GetInRadius(transform.position, _explosionRadius))
        {
            e.Damage(_entity.Stats.GetDamage(_explosionDamage));
        }

        base.OnEnemyCollision(enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
