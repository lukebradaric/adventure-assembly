using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "HeroData")]
    public class HeroData : SerializedScriptableObject
    {
        [OdinSerialize] public string Name { get; private set; }

        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [PreviewField(128, ObjectFieldAlignment.Left)]
        [OdinSerialize] public Sprite Sprite { get; private set; }

        [OdinSerialize] public ClassData ClassData { get; private set; }

        [OdinSerialize] public int MaxHealth { get; private set; }
    }
}