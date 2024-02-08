using AdventureAssembly.Core;
using AdventureAssembly.Units.Abilities;
using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Classes;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.ScriptableSounds;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "HeroData")]
    public class HeroData : CharacterData
    {
        [BoxGroup("General")]
        [OdinSerialize] public Color BackgroundColor { get; set; } = Color.magenta;

        [BoxGroup("General")]
        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [BoxGroup("Stats")]
        [OdinSerialize] public float AbilitySpeed { get; private set; } = 1f;

        [BoxGroup("Audio")]
        [OdinSerialize] public ScriptableSound AbilitySound { get; private set; } = null;

        [BoxGroup("Components")]
        [OdinSerialize] public List<ClassData> ClassData { get; private set; } = new List<ClassData>();

        [BoxGroup("Components")]
        [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();
    }
}