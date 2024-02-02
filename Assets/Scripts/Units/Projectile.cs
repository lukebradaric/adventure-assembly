using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using TinyTools.ScriptableVariables;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Projectile : MonoBehaviour
    {
        private const float MaxLifetime = 5.0f;

        [Space]
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private StringScriptableVariable _enemyTag;

        [Space]
        [Header("Settings")]
        [SerializeField] private float _speed;

        public Hero Hero { get; protected set; }
        public CharacterUnit TargetUnit { get; protected set; }
        public int BaseDamage { get; protected set; }

        public Vector2 MoveDirection { get; protected set; }

        // This Ititalize homes to a set target.
        public void Initialize(Hero hero, int baseDamage, CharacterUnit targetUnit)
        {
            this.Hero = hero;
            this.BaseDamage = baseDamage;
            this.TargetUnit = targetUnit;
        }

        // This Intialize doesn't need a target, instead it moves in the direction we give it.
        public void Initialize(Hero hero, int baseDamage, Vector2 direction)
        {
            this.Hero = hero;
            this.BaseDamage = baseDamage;
            this.TargetUnit = null;
    
            SetMoveDirection(direction);
        }

        public void SetMoveDirection(Vector2 direction)
        {
            MoveDirection = direction.normalized;

            // Face the top of the projectile towards the direction we want it to go
            transform.up = MoveDirection;

            // Set projectile speed
            _rigidbody.velocity = MoveDirection * _speed;
        }

        private void Start()
        {
            // Destroy projectile after maxlifetime
            Destroy(gameObject, MaxLifetime);
        }

        private void FixedUpdate()
        {
            if (TargetUnit == null)
            {
                return;
            }

            SetMoveDirection((TargetUnit.transform.position - transform.position));
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
            // Create new damagedata
            DamageData damageData = new DamageData(Hero, enemy, BaseDamage);
            //DamageData damageData = Hero.Stats.GetDamageData(new DamageData(Hero, enemy, BaseDamage));

            // Assign the damage direction to the direction of this projectile
            damageData.Direction = MoveDirection;

            enemy.TakeDamage(damageData);

            Destroy(gameObject);
        }
    }
}