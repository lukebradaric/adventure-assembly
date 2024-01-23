using AdventureAssembly.Core;
using AdventureAssembly.Units.Abilities;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "HeroData")]
    public class HeroData : SerializedScriptableObject
    {
        [PropertySpace]
        [OdinSerialize] public string Name { get; private set; }

        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [PreviewField(128, ObjectFieldAlignment.Left)]
        [OdinSerialize] public Sprite Sprite { get; private set; }

        [PropertySpace]
        [Title("Stats")]
        [OdinSerialize] public int MaxHealth { get; private set; }

        [PropertySpace]
        [Title("Data")]
        [OdinSerialize] public ClassData ClassData { get; private set; }

        [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    }
}