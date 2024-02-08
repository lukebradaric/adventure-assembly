using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using AdventureAssembly.Units.Heroes;
using System;
using System.Collections;
using System.Collections.Generic;
using TinyTools.ScriptableVariables;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace AdventureAssembly.Units
{
    public class Projectile : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CircleCollider2D _circleCollider;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private StringScriptableVariable _enemyTag;

        public ProjectileData ProjectileData { get; protected set; }
        public Hero Hero { get; protected set; }

        public List<ProjectileComponent> Components { get; protected set; } = new List<ProjectileComponent>();
        public List<ParticleSystem> ParticlePrefabs { get; protected set; } = new List<ParticleSystem>();
        public List<TrailRenderer> TrailPrefabs { get; protected set; } = new List<TrailRenderer>();

        private Character _targetCharacter = null;
        public Character TargetCharacter
        {
            get
            {
                return _targetCharacter;
            }
            set
            {
                _targetCharacter = value;

                if (_targetCharacter == null)
                {
                    return;
                }

                MoveDirection = (_targetCharacter.transform.position - transform.position);
            }
        }

        private Vector2 _moveDirection;
        public Vector2 MoveDirection
        {
            get
            {
                return _moveDirection;
            }
            set
            {
                _moveDirection = value.normalized;
                transform.up = _moveDirection;
                _rigidbody.velocity = _moveDirection * ProjectileData.Speed;
            }
        }

        public Action EnemyCollision;

        public void Initialize(ProjectileData projectileData, Hero hero, Vector2 direction)
        {
            this.ProjectileData = projectileData;
            this.Hero = hero;
            this.MoveDirection = direction;

            _spriteRenderer.sprite = ProjectileData.Sprite;
            _spriteRenderer.color = ProjectileData.Color;
            _circleCollider.radius = ProjectileData.ColliderRadius;

            // Add projectile components to this object to listen to events
            foreach (ProjectileComponent component in ProjectileData.Components)
            {
                ProjectileComponent newComponent = component.GetClone();
                Components.Add(newComponent);
                newComponent.Initialize(this);
                newComponent.OnEnable();
            }

            // Instantiate particle prefabs on this projectile
            foreach (ParticleSystem particlePrefab in ProjectileData.ParticlePrefabs)
            {
                ParticlePrefabs.Add(Instantiate(particlePrefab, this.transform));
            }

            // Instantiate trail renderer prefabs on this projectile
            foreach (TrailRenderer trailPrefab in ProjectileData.TrailPrefabs)
            {
                TrailPrefabs.Add(Instantiate(trailPrefab, this.transform));
            }

            Destroy(ProjectileData.MaxLifetime);
        }

        /// <summary>
        /// Destroys this projectile.
        /// </summary>
        /// <param name="seconds">The amount of seconds to wait before destroying.</param>
        private void Destroy(float seconds = 0f)
        {
            if (seconds != 0f)
            {
                StartCoroutine(DestroyCoroutine(seconds));
                return;
            }

            OnBeforeDestroy();
            Destroy(gameObject);
        }

        private IEnumerator DestroyCoroutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            OnBeforeDestroy();
            Destroy(gameObject);
        }

        /// <summary>
        /// Called right before projectile is destroyed. Do not change this. It is a weird hacky solution due to the order of hierarchy calls.
        /// </summary>
        private void OnBeforeDestroy()
        {
            // Disable all components on this projectile
            foreach (ProjectileComponent component in Components)
            {
                component.OnDisable();
            }

            foreach (ParticleSystem particleSystem in ParticlePrefabs)
            {
                particleSystem.transform.parent = null;
                MainModule mainModule = particleSystem.main;
                mainModule.stopAction = ParticleSystemStopAction.Destroy;
                particleSystem.Stop();
            }
        }

        private void FixedUpdate()
        {
            // If we have a specific target character, continously move towards it
            if (TargetCharacter == null)
            {
                return;
            }

            MoveDirection = (TargetCharacter.transform.position - transform.position);
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
            DamageData damageData = new DamageData(Hero, enemy, ProjectileData.BaseDamage);

            // Assign the damage direction to the direction of this projectile
            damageData.Direction = MoveDirection;

            enemy.TakeDamage(damageData);

            EnemyCollision?.Invoke();

            if (ProjectileData.DestroyOnCollision)
            {
                Destroy();
            }

            TargetCharacter = null;
        }
    }
}