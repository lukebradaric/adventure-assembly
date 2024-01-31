using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "EnemyData")]
    public class EnemyData : CharacterUnitData
    {
        [PropertySpace]
        [Title("Enemy")]
        [OdinSerialize] public int KillExperience { get; private set; }

        [OdinSerialize] public int BaseDamage { get; private set; }

        [OdinSerialize] public EnemyNavigation Navigation { get; private set; } = new DefaultEnemyNavigation();
    }
}