using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// Projectile component to increase the damage of a projectile based on the distance it travels.
    /// </summary>
    public class DistanceDamageProjectileComponent : ProjectileComponent
    {
        [BoxGroup("Settings")]
        [Tooltip("How much should the damage of the projectile increase for each world unit it traveled?")]
        [SerializeField] private float _damageMultiplierPerUnitTraveled;

        private Vector2 _startPosition;

        public override void OnEnable()
        {
            _startPosition = _projectile.transform.position;

            _projectile.BeforeCollision += OnBeforeCollision;
        }

        public override void OnDisable()
        {
            _projectile.BeforeCollision -= OnBeforeCollision;
        }

        private void OnBeforeCollision()
        {
            float distance = Vector2.Distance(_startPosition, _projectile.transform.position);
            _projectile.DamageMultiplier += distance * _damageMultiplierPerUnitTraveled;
        }
    }
}