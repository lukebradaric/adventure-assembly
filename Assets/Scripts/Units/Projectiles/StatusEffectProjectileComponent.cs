using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// A projectile component that adds a status effect to an enemy when it is hit.
    /// </summary>
    public class StatusEffectProjectileComponent : ProjectileComponent
    {
        [BoxGroup("Settings")]
        [SerializeField] private StatusEffects _statusEffect;

        [BoxGroup("Settings")]
        [SerializeField] private bool _isTemporary;
        [BoxGroup("Settings")]
        [ShowIf(nameof(_isTemporary))]
        [SerializeField] private float _duration;

        public override void OnEnable()
        {
            _projectile.EnemyCollision += OnEnemyCollision;
        }

        public override void OnDisable()
        {
            _projectile.EnemyCollision -= OnEnemyCollision;
        }

        private void OnEnemyCollision(Enemy enemy)
        {
            if (_isTemporary)
            {
                enemy.StatusEffects.Add(_statusEffect, _duration);
                return;
            }

            enemy.StatusEffects.Add(_statusEffect);
        }
    }
}