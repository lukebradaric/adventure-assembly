using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using Sirenix.OdinInspector;
using TinyTools.Extensions;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    /// <summary>
    /// Heals a random hero when this projectile kills an enemy.
    /// </summary>
    public class HealOnKillProjectileComponent : ProjectileComponent
    {
        [BoxGroup("Settings")]
        [SerializeField] private int _baseHeal;

        [BoxGroup("Audio")]
        [SerializeField] private ScriptableSound _healSound;

        public override void OnEnable()
        {
            _projectile.EnemyCollision += OnEnemyCollision;
        }

        public override void OnDisable()
        {
            _projectile.EnemyCollision -= OnEnemyCollision;
        }

        private void OnEnemyCollision(Enemy enemy)
        {
            if (!enemy.IsDead)
            {
                return;
            }

            Hero hero = HeroManager.Instance.Units.Random();
            HealData healData = new HealData(_projectile.Hero, hero, _baseHeal);
            hero.Heal(healData);
            _healSound?.Play();
        }
    }
}