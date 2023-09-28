using UnityEngine;

public class TrapperObject : Projectile
{
    [SerializeField] private int _stunDuration;

    protected override void FixedUpdate()
    {
        //Don't do anything, just needed to override for simplicity
    }

    protected override void OnEnemyCollision(Enemy enemy)
    {
        enemy.Damage(_entity.Stats.GetDamage(_damage));

        // If we killed enemy with trap damage, return
        if (enemy.IsDead)
        {
            _destroySound?.Play();
            Destroy(gameObject);
            return;
        }

        enemy.AddStun(_stunDuration);
        if (_destroyParticlesPrefab != null)
        {
            var particle = Instantiate(_destroyParticlesPrefab, this.transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().startLifetime = _stunDuration * TurnManager.TurnInterval;
        }
        _destroySound?.Play();
        Destroy(gameObject);
    }
}
