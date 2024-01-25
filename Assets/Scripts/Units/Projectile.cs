using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using TinyTools.ScriptableVariables;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Projectile : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private StringScriptableVariable _enemyTag;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _speed;

        public Hero Hero { get; protected set; }
        public Unit TargetUnit { get; protected set; }
        public int BaseDamage { get; protected set; }

        public Vector2 MoveDirection { get; protected set; }

        public void Initialize(Hero hero, Unit targetUnit, int baseDamage)
        {
            this.Hero = hero;
            this.TargetUnit = targetUnit;
            this.BaseDamage = baseDamage;
        }

        private void FixedUpdate()
        {
            if (TargetUnit == null)
            {
                return;
            }

            MoveDirection = (TargetUnit.transform.position - transform.position).normalized;
            transform.up = MoveDirection;
            _rigidbody.velocity = MoveDirection * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.CompareTag(_enemyTag.Value))
            {
                return;
            }

            if (!collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                return;
            }

            OnEnemyCollision(enemy);
        }

        private void OnEnemyCollision(Enemy enemy)
        {
            enemy.Damage(Hero.Stats.GetDamageData(new DamageData(BaseDamage)));
            Destroy(gameObject);
        }
    }
}