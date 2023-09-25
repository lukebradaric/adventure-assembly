using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingProjectile : Projectile
{
    protected override void OnEnemyCollision(Enemy enemy)
    {
        enemy.Damage(_damage);
        if (_destroyParticlesPrefab != null)
        {
            Instantiate(_destroyParticlesPrefab, transform.position, Quaternion.identity);
        }
        _target = null;
    }

}
