using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "EnemyData")]
    public class EnemyData : UnitData
    {
        [OdinSerialize] public int KillExperience { get; private set; }

        [PropertySpace]
        [Title("Stats")]
        [OdinSerialize] public int MaxHealth { get; private set; }

        [OdinSerialize] public int BaseDamage { get; private set; }

        [PropertySpace]
        [Title("Movement")]
        [OdinSerialize] public EnemyNavigation Navigation { get; private set; } = new DefaultEnemyNavigation();
    }
}