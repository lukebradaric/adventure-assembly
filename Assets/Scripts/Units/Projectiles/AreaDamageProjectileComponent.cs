using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// Projectile component that drops a lingering area damage component on impact.
    /// </summary>
    public class AreaDamageProjectileComponent : ProjectileComponent
    {
        [BoxGroup("Prefabs")]
        [SerializeField] private AreaDamage _areaDamagePrefab;

        public override void OnEnable()
        {
            _projectile.Collision += OnCollision;
        }

        public override void OnDisable()
        {
            _projectile.Collision -= OnCollision;
        }

        private void OnCollision()
        {
            AreaDamage areaDamage = GameObject.Instantiate(_areaDamagePrefab, _projectile.transform.position, Quaternion.identity);
            areaDamage.Source = _projectile.Hero;
        }
    }
}