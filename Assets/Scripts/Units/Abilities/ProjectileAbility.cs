using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] private Projectile _projectilePrefab;

        protected override void Execute()
        {
            // Fire projectile towards nearest target
            // TODO: Implement projectile firing logic :D
        }
    }
}