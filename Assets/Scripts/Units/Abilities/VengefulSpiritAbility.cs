using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TinyTools.ScriptableEvents;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class VengefulSpiritAbility : Ability
    {
        [BoxGroup("Events")]
        [SerializeField] private GameScriptableEvent _onHeroDied;

        [BoxGroup("Settings")]
        [SerializeField] private float _baseRadius;
        [BoxGroup("Settings")]
        [SerializeField] private int _baseDamage;
        [BoxGroup("Settings")]
        [SerializeField] private float _increasePerStack = 0.2f;

        [BoxGroup("Components")]
        [SerializeField] private GameObject _particlePrefab;

        [BoxGroup("Audio")]
        [SerializeField] private ScriptableSound _sound;

        private int _stacks = 0;

        public override void OnEnable()
        {
            _onHeroDied.Event += OnHeroDied;
        }

        public override void OnDisable()
        {
            _onHeroDied.Event -= OnHeroDied;
        }

        protected override bool Execute()
        {
            _stacks++;
            return true;
        }

        private float GetIncreaseMultiplier()
        {
            return 1 + (_stacks * _increasePerStack);
        }

        private void OnHeroDied(GameEventData gameEventData)
        {
            // Do sick ass attack here
            List<Enemy> enemies = EnemyManager.Instance.GetUnitsInRadius(_hero.transform.position, _baseRadius * GetIncreaseMultiplier());

            GameObject particle = GameObject.Instantiate(_particlePrefab, _hero.transform);
            particle.transform.localScale = particle.transform.localScale * GetIncreaseMultiplier();

            foreach (Enemy enemy in enemies)
            {
                enemy.TakeDamage(new DamageData(_hero, enemy, (int)(_baseDamage * GetIncreaseMultiplier())));
            }
            _stacks = 0;
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_hero.transform.position, _baseRadius * GetIncreaseMultiplier());
        }
    }
}