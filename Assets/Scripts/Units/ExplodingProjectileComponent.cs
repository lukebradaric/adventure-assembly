using UnityEngine;

namespace AdventureAssembly.Units
{
    public class ExplodingProjectileComponent : ProjectileComponent
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _baseDamage;

        public override void OnEnable()
        {
            _projectile.EnemyCollision += OnEnemyCollision;
        }

        public override void OnDisable()
        {
            _projectile.EnemyCollision -= OnEnemyCollision;
        }

        private void OnEnemyCollision()
        {
            // Do damage in radius
            Debug.Log($"Explosion! Radius:{_radius} Damage:{_baseDamage}");
        }
    }
}