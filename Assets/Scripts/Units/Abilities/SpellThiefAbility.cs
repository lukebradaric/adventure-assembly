using AdventureAssembly.Units.Enemies;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{

    public class SpellThiefAbility : Ability
    {
        [SerializeField] private ProjectileData _projectileData;

        [Tooltip("How many enemies should there be er extra projectile?")]
        [SerializeField] private int _enemiesPerExtraProjectile;

        protected override void Execute()
        {
            // Calculate how many times to fire the projectile and add one to always have at least one projectile
            int count = (int)(EnemyManager.Units.Count / _enemiesPerExtraProjectile) + 1;

            for (int i = 0; i < count; i++)
            {
                // Calculate a random direction
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

                // In the rare chance that the random vector is (0,0), set it to going straight up!
                if (direction == Vector2.zero)
                {
                    direction = Vector2.up;
                }

                // Fire the projectile in direction
                _projectileData.Create(_hero, direction);
            }
        }
    }

}
