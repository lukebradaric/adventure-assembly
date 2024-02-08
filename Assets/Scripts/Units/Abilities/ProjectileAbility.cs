﻿using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    [System.Serializable]
    public class ProjectileAbility : Ability
    {
        [BoxGroup("Data")]
        [SerializeField] private ProjectileData _projectileData;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            _projectileData.Create(_hero, enemy);
            return true;
        }
    }
}