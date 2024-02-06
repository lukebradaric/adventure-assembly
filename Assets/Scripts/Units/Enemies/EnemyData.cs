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
        [PropertySpace]
        [Title("Enemy")]
        [OdinSerialize] public int KillExperience { get; private set; }

        [OdinSerialize] public int BaseDamage { get; private set; }

        [Tooltip("What animation should play when this Enemy attacks?")]
        [OdinSerialize] public UnitTween AttackTween { get; private set; } = new DefaultEnemyAttackTween();

        [Tooltip("What navigation method should this Enemy use for moving?")]
        [OdinSerialize] public EnemyNavigation Navigation { get; private set; } = new DefaultEnemyNavigation();
    }
}