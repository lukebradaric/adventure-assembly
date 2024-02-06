using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    [System.Serializable]
    public class ProjectileAbility : Ability
    {
        [BoxGroup("Settings")]
        [SerializeField] private int _baseDamage;
        [BoxGroup("Settings")]
        [SerializeField] private float _baseSpeed;
        [BoxGroup("Prefabs")]
        [SerializeField] private Projectile _projectilePrefab;

        protected override void Execute()
        {
            Enemy enemy = EnemyManager.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return;
            }

            Projectile projectile = GameObject.Instantiate(_projectilePrefab, _hero.transform.position, Quaternion.identity);
            projectile.Initialize(_baseDamage, _baseSpeed, _hero, enemy);
        }
    }
}