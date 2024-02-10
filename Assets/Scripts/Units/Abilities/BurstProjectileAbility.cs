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
        [ShowIf(nameof(_randomBetweenTwoConstants))]
        [SerializeField] private int _minProjectiles;
        
        [BoxGroup("Settings")]
        [ShowIf(nameof(_randomBetweenTwoConstants))]
        [SerializeField] private int _maxProjectiles;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            CoroutineManager.Instance.StartCoroutine(FireCoroutine(enemy));
            return true;
        }

        private IEnumerator FireCoroutine(Enemy enemy)
        {
            // number of times to shoot
            for (int i = 0; i < Random.Range(_minProjectiles, _maxProjectiles + 1); i++)
            {
                // If there's no enemy left, stop shooting
                if (enemy == null)
                {
                    yield break;
                }
                _projectileData.Create(_hero, enemy);
                yield return new WaitForSeconds(_secondsBetweenShots);
                
            }
        }
    }

}
