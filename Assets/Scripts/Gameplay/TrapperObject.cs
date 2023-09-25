using System.Collections;
using System.Collections.Generic;
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
        enemy.AddStun(_stunDuration);
        enemy.Damage(_damage);
        if (_destroyParticlesPrefab != null)
        {
            var particle = Instantiate(_destroyParticlesPrefab, this.transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().startLifetime = _stunDuration * TurnManager.TurnInterval;
        }
        _destroySound?.Play();
        Destroy(gameObject);
    }
}
