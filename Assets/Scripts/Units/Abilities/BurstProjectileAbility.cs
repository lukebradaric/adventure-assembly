using AdventureAssembly.Core;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Projectiles;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class BurstProjectileAbility : Ability
    {
        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _projectileData;

        [BoxGroup("Settings")]
        [SerializeField] private float _secondsBetweenShots;

        [BoxGroup("Settings")]
        [SerializeField] private bool _randomBetweenTwoConstants;

        [BoxGroup("Settings")]
        [HideIf(nameof(_randomBetweenTwoConstants))]
        [SerializeField] private int _projectileCount;

        [BoxGroup("Settings")]
        [ShowIf(nameof(_randomBetweenTwoConstants))]
        [SerializeField] private int _minProjectiles;

        [BoxGroup("Settings")]
        [ShowIf(nameof(_randomBetweenTwoConstants))]
        [SerializeField] private int _maxProjectiles;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.Instance.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            CoroutineManager.Instance.StartCoroutine(FireCoroutine(enemy));
            return true;
        }

        private IEnumerator FireCoroutine(Enemy enemy)
        {
            Vector2 direction = (enemy.transform.position - _hero.transform.position).normalized;

            int count = _randomBetweenTwoConstants ? Random.Range(_minProjectiles, _maxProjectiles + 1) : _projectileCount;

            // number of times to shoot
            for (int i = 0; i < count; i++)
            {
                // If there's no enemy left, stop shooting
                if (enemy != null)
                {
                    _projectileData.Create(_hero, enemy);
                }
                else
                {
                    _projectileData.Create(_hero, direction);
                }

                yield return new WaitForSeconds(_secondsBetweenShots);

            }
        }
    }

}
