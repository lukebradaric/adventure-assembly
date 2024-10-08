﻿using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Heroes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Projectiles
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ProjectileData")]
    public class ProjectileData : SerializedScriptableObject
    {
        [BoxGroup("Settings")]
        [Tooltip("The prefab to spawn when creating a projectile. It is unlikely you will ever change this value.")]
        [SerializeField] private Projectile _prefab = default;

        [BoxGroup("Settings")]
        [PreviewField(128, ObjectFieldAlignment.Left)]
        [Tooltip("What sprite should this projectile have?")]
        [OdinSerialize] public Sprite Sprite { get; protected set; }

        [BoxGroup("Settings")]
        [Tooltip("What color should the sprite renderer be?")]
        [OdinSerialize] public Color Color { get; protected set; } = Color.white;

        [BoxGroup("Game Settings")]
        [Tooltip("What is the base damage of this projectile?")]
        [OdinSerialize] public int BaseDamage { get; protected set; } = 1;

        [BoxGroup("Game Settings")]
        [Tooltip("What is the speed of this projectile?")]
        [OdinSerialize] public float Speed { get; protected set; } = 10f;

        [BoxGroup("Game Settings")]
        [Tooltip("How should the projectile determine its rotation?")]
        [OdinSerialize] public ProjectileRotationMode RotationMode { get; protected set; }

        [BoxGroup("Game Settings")]
        [Tooltip("What is the angular speed of this projectile?")]
        [ShowIf(nameof(RotationMode), ProjectileRotationMode.Free)]
        [OdinSerialize] public float RotationSpeed { get; protected set; } = 0f;

        [BoxGroup("Extra Settings")]
        [Tooltip("What is the collision radius of this projectiles circle collider?")]
        [OdinSerialize] public bool DestroyOnCollision { get; protected set; } = true;

        [BoxGroup("Extra Settings")]
        [Tooltip("What is the collision radius of this projectiles circle collider?")]
        [OdinSerialize] public float ColliderRadius { get; protected set; } = 0.25f;

        [BoxGroup("Extra Settings")]
        [Tooltip("After how long should this projectile be forcibly destroye? Measured in seconds.")]
        [OdinSerialize] public float MaxLifetime { get; protected set; } = 5.0f;

        [BoxGroup("Audio")]
        [Tooltip("What sound should play when this projectile collides with enemy?")]
        [OdinSerialize] public ScriptableSound CollisionSound { get; protected set; } = null;

        [BoxGroup("Components")]
        [OdinSerialize] public List<ProjectileComponent> Components { get; protected set; } = new List<ProjectileComponent>();

        [BoxGroup("Components")]
        [Tooltip("What particle prefabs should be instantiated under this projectile?")]
        [OdinSerialize] public List<ParticleSystem> ParticlePrefabs { get; protected set; } = new List<ParticleSystem>();

        [BoxGroup("Components")]
        [Tooltip("What trail renderer prefabs should be instantiated under this projectile?")]
        [OdinSerialize] public List<TrailRenderer> TrailPrefabs { get; protected set; } = new List<TrailRenderer>();

        /// <summary>
        /// Creates a projectile from this ProjectileData and fires it towards a target.
        /// </summary>
        /// <param name="hero">The hero that fired this projectile</param>
        /// <param name="targetCharacter">The target to move the projectile towards</param>
        /// <returns></returns>
        public Projectile Create(Hero hero, Character targetCharacter)
        {
            Projectile projectile = Create(hero, targetCharacter.transform.position - hero.transform.position);
            projectile.TargetCharacter = targetCharacter;
            return projectile;
        }

        /// <summary>
        /// Creates a projectile from this ProjectileData and fires it in a direction.
        /// </summary>
        /// <param name="hero">The hero that fired this projectile</param>
        /// <param name="direction">The directon to move the projectile</param>
        /// <returns></returns>
        public Projectile Create(Hero hero, Vector2 direction)
        {
            Projectile projectile = GameObject.Instantiate(_prefab, hero.transform.position, Quaternion.identity);
            projectile.Initialize(this, hero, direction);
            return projectile;
        }
    }
}