using AdventureAssembly.Units.Heroes;
using Sirenix.OdinInspector;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// Heals all heroes that this projectile trigger overlaps with
    /// </summary>
    public class HealOnTriggerProjectileComponent : ProjectileComponent
    {
        [BoxGroup("Settings")]
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