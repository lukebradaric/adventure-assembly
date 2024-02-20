using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// Projectile component that explodes on impact, dealing immediate damage to nearby enemies.
    /// </summary>
    public class ExplodingProjectileComponent : ProjectileComponent
    {
        [BoxGroup("Prefabs")]
        [SerializeField] private GameObject _particlePrefab;

        [BoxGroup("Settings")]
        [SerializeField] private int _baseDamage;
        [BoxGroup("Settings")]
        [SerializeField] private float _baseRadius;

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
            GameObject.Instantiate(_particlePrefab, _projectile.transform.position, Quaternion.identity);
            foreach (Character character in EnemyManager.Instance.GetUnitsInRadius(_projectile.transform.position, _baseRadius))
            {
                DamageData damageData = new DamageData(_projectile.Hero, character, _baseDamage);
                character.TakeDamage(damageData);
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_projectile.transform.position, _baseRadius);
        }
    }
}