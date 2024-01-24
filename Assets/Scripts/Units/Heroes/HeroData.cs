using AdventureAssembly.Core;
using AdventureAssembly.Units.Abilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "HeroData")]
    public class HeroData : UnitData
    {
        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [PropertySpace]
        [Title("Stats")]
        [OdinSerialize] public int MaxHealth { get; private set; }

        [PropertySpace]
        [Title("Data")]
        [OdinSerialize] public ClassData ClassData { get; private set; }

        [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    }
}