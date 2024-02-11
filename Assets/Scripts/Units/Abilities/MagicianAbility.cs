using AdventureAssembly.Units.Bosses;
using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class MagicianAbility : Ability
    {
        [BoxGroup("Prefabs")]
        [SerializeField] private GameObject _disappearParticlePrefab;

        [BoxGroup("Settings")]
        [SerializeField] private int _baseDamage = 1;
        [BoxGroup("Settings")]
        [SerializeField] private float _disappearChance = 0.1f;
        [BoxGroup("Settings")]
        [SerializeField] private int _disappearDamage = 999;

        [BoxGroup("Audio")]
        [SerializeField] private ScriptableSound _disappearSound;

        protected override bool Execute()
        {
            Enemy enemy = EnemyManager.Instance.GetNearestUnit(_hero.Position);

            if (enemy == null)
            {
                return false;
            }

            DamageData damageData = new DamageData(_hero, enemy, _baseDamage);

            // If the enemy is not a boss, and disappear chance hits, change damage value
            if (enemy is not Boss && _hero.Stats.GetLuck(_disappearChance) > Random.value)
            {
                _disappearSound?.Play();
                GameObject.Instantiate(_disappearParticlePrefab, enemy.transform.position, Quaternion.identity);
                damageData.Value = _disappearDamage;
            }

            enemy.TakeDamage(damageData);

            return true;
        }
    }
}