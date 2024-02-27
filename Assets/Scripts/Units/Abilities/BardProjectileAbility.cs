using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Projectiles;
using Sirenix.OdinInspector;
using TinyTools.Generics;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class BardProjectileAbility : Ability
    {
        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _projectile1Data;

        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _projectile2Data;

        [BoxGroup("Settings")]
        [SerializeField] private int _projectile1Weight;

        [BoxGroup("Settings")]
        [SerializeField] private int _projectile2Weight;

        [BoxGroup("Settings")]
        [SerializeField] private int _projectile3Weight;

        [BoxGroup("Audio")]
        [SerializeField] private ScriptableSound _nothingSound;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.Instance.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            WeightedList<ProjectileData> weightedProjectiles = new WeightedList<ProjectileData>();

            weightedProjectiles.Add(_projectile1Data, _projectile1Weight);
            weightedProjectiles.Add(_projectile2Data, (int)_hero.Stats.GetLuck(_projectile2Weight));
            weightedProjectiles.Add(null, (int)(_projectile3Weight / _hero.Stats.LuckMultiplier.Value * 2));

            ProjectileData projectileData = weightedProjectiles.GetRandom();

            if (projectileData == null)
            {
                _nothingSound?.Play();
                return true;
            }

            projectileData.Create(_hero, enemy);
            return true;
        }
    }

}
