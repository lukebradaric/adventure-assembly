using AdventureAssembly.Core;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Projectiles;
using Sirenix.OdinInspector;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class BardProjectileAbility : Ability
    {
        [SerializeField] private bool _has5Projectiles;

        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _projectile1Data;

        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _projectile2Data;

        [BoxGroup("Settings")]
        [SerializeField] private float _projectile1Weight;

        [BoxGroup("Settings")]
        [SerializeField] private float _projectile2Weight;

        [BoxGroup("Settings")]
        [SerializeField] private float _projectile3Weight;

        [BoxGroup("Audio")]
        [SerializeField] private ScriptableSound _nothingSound;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.Instance.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            // this should hopefully add up to 1
            float random = Random.Range(0.0f, _projectile1Weight + _projectile2Weight + _projectile3Weight);
            
            if (random <= _projectile1Weight)
            {
                // shoot projectile 1
                _projectile1Data.Create(_hero, enemy);

            } else if ((random > _projectile1Weight) && (random <= _projectile2Weight))
            {
                // shoot projectile 2
                _projectile2Data.Create(_hero, enemy);

            } else 
            {
                // do nothing!!! :D
                _nothingSound?.Play();
            }
            Debug.Log(random);
            return true;
        }
    }

}
