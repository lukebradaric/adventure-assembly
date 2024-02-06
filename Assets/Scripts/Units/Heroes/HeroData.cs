using AdventureAssembly.Core;
using AdventureAssembly.Units.Abilities;
using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Classes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "HeroData")]
    public class HeroData : CharacterData
    {
        [PropertySpace]
        [Title("Hero")]
        [OdinSerialize] public Color BackgroundColor { get; set; } = Color.magenta;

        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [OdinSerialize] public List<ClassData> ClassData { get; private set; } = new List<ClassData>();

        [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    }
}