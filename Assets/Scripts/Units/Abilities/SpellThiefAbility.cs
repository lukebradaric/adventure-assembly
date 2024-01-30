using System.Collections;
using System.Collections.Generic;
using AdventureAssembly.Units.Enemies;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities{
    
    public class SpellThiefAbility : Ability
    {
        // Prefab of the Projectile
        [SerializeField] private Projectile _projectilePrefab;

        [Tooltip("The base damage of the projectile.")]
        [SerializeField] private int _baseDamage;

        [Tooltip("How many enemies should there be er extra projectile?")]
        [SerializeField] private int _enemiesPerExtraProjectile;

        protected override void Execute()
        {
            // Calculate how many times to fire the projectile and add one to always have at least one projectile
            int count = (int)(EnemyManager.Units.Count / _enemiesPerExtraProjectile) + 1;

            for (int i = 0; i < count; i++)
            {
                // Spawn the projectile
                Projectile projectile = GameObject.Instantiate(_projectilePrefab, (Vector2)_hero.Position, Quaternion.identity);

                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                // In the rare chance that the random vector is (0,0), set it to going straight up!
                if (direction == Vector2.zero)
                {
                    direction = Vector2.up;
                }

                // Initialize projectile
                projectile.Initialize(_hero, _baseDamage, direction);
            }
        }
    }
    
}
