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
        [PropertySpace]
        [Title("Hero")]
        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [OdinSerialize] public List<ClassData> ClassData { get; private set; } = new List<ClassData>();

        [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    }
}