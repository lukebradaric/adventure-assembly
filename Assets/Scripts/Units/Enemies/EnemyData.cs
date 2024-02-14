using AdventureAssembly.Core;
using AdventureAssembly.Units.Animation;
using AdventureAssembly.Units.Characters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "EnemyData")]
    public class EnemyData : CharacterData
    {
        [BoxGroup("Stats")]
        [Tooltip("How much experience should the player be awarded for killing this enemy?")]
        [OdinSerialize] public int KillExperience { get; private set; }

        [BoxGroup("Stats")]
        [Tooltip("What is the base damage this enemy does to heroes?")]
        [OdinSerialize] public int BaseDamage { get; private set; }

        [BoxGroup("Stats")]
        [Tooltip("What is the base chance that this enemy drops gold?")]
        [OdinSerialize] public float BaseGoldDropChance { get; private set; } = 0.05f;

        [BoxGroup("Animation")]
        [Tooltip("What animation should play when this Enemy attacks?")]
        [OdinSerialize] public UnitTween AttackTween { get; private set; } = new DefaultEnemyAttackTween();

        [BoxGroup("Animation")]
        [Tooltip("What particles should spawn when this enemy dies?")]
        [OdinSerialize] public ParticleSystem DeathParticles { get; private set; } = default;

        [BoxGroup("Pathfinding")]
        [Tooltip("What navigation method should this Enemy use for moving?")]
        [OdinSerialize] public EnemyNavigation Navigation { get; private set; } = new DefaultEnemyNavigation();

        public virtual Enemy Create(Vector2Int position)
        {
            Enemy enemy = (Enemy)Instantiate(Prefab, (Vector2)position, Quaternion.identity);
            enemy.Initialize(this, position);
            return enemy;
        }
    }
}