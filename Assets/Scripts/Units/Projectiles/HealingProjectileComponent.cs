using AdventureAssembly.Units.Heroes;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// Heals all heroes that this projectile overlaps with.
    /// </summary>
    public class HealingProjectileComponent : ProjectileComponent
    {
        [SerializeField] private int _baseHeal;

        public override void OnEnable()
        {
            _projectile.HeroTrigger += OnHeroTrigger;
        }

        public override void OnDisable()
        {
            _projectile.HeroTrigger -= OnHeroTrigger;
        }

        private void OnHeroTrigger(Hero hero)
        {
            HealData healData = new HealData(_projectile.Hero, hero, _baseHeal);
            hero.Heal(healData);
        }
    }
}