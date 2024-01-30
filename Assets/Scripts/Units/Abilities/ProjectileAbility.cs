using AdventureAssembly.Units.Enemies;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    [System.Serializable]
    public class ProjectileAbility : Ability
    {
        [SerializeField] private int _baseDamage;
        [SerializeField] private Projectile _projectilePrefab;

        protected override void Execute()
        {
            Enemy enemy = EnemyManager.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return;
            }

            Projectile projectile = GameObject.Instantiate(_projectilePrefab, _hero.transform.position, Quaternion.identity);
            projectile.Initialize(_hero, _baseDamage, enemy);
        }
    }
}