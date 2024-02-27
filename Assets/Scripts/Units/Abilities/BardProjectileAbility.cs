using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Projectiles;
using Sirenix.OdinInspector;
using TinyTools.Generics;
using TinyTools.ScriptableSounds;
using UnityEngine;
using System.Collections.Generic;
using TinyTools.Extensions;

namespace AdventureAssembly.Units.Abilities
{
    public class BardProjectileAbility : Ability
    {
        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _normalProjectileData;

        [BoxGroup("Settings")]
        [SerializeField] private ProjectileData _specialProjectileData;

        [BoxGroup("Settings")]
        [SerializeField] private int _normalProjectileWeight;

        [BoxGroup("Settings")]
        [SerializeField] private int _specialProjectileWeight;

        [BoxGroup("Settings")]
        [SerializeField] private int _nothingWeight;

        [BoxGroup("Audio")]
        [SerializeField] private List<ScriptableSound> _normalProjectileSounds = new List<ScriptableSound>();
        [SerializeField] private ScriptableSound _specialProjectileSound;
        [SerializeField] private ScriptableSound _nothingSound;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.Instance.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            WeightedList<ProjectileData> weightedProjectiles = new WeightedList<ProjectileData>();

            weightedProjectiles.Add(_normalProjectileData, _normalProjectileWeight);
            weightedProjectiles.Add(_specialProjectileData, (int)_hero.Stats.GetLuck(_specialProjectileWeight));
            weightedProjectiles.Add(null, (int)(_nothingWeight / (_hero.Stats.LuckMultiplier.Value)));

            ProjectileData projectileData = weightedProjectiles.GetRandom();

            if (projectileData == _normalProjectileData)
            {
                _normalProjectileSounds.Random()?.Play();
            } else if (projectileData == _specialProjectileData)
            {
                _specialProjectileSound?.Play();
            }

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
