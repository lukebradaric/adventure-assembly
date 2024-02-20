using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class AreaDamage : MonoBehaviour
    {
        [SerializeField] private int _baseDamage;
        [SerializeField] private float _radius;
        [SerializeField] private float _tickRate;
        [SerializeField] private float _lifetime;

        // The character who is dealing this damage
        public Character Source { get; set; }

        private void OnEnable()
        {
            StartCoroutine(TickCoroutine());
            StartCoroutine(LifetimeCoroutine());
        }

        private IEnumerator TickCoroutine()
        {
            yield return new WaitForSeconds(_tickRate);

            List<Enemy> enemies = EnemyManager.Instance.GetUnitsInRadius(transform.position, _radius);
            foreach (Enemy enemy in enemies)
            {
                enemy.TakeDamage(new DamageData(Source, enemy, _baseDamage));
            }

            StartCoroutine(TickCoroutine());
        }

        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(_lifetime);
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}