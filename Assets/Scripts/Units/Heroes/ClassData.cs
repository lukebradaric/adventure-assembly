﻿using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ClassData")]
    public class ClassData : SerializedScriptableObject
    {
        [OdinSerialize] public string Name { get; private set; }

        [MultiLineProperty]
        [OdinSerialize] public string Description { get; private set; }

        [OdinSerialize] public Color Color { get; private set; }

        [OdinSerialize] public List<ClassModifier> Modifiers { get; private set; } = new List<ClassModifier>();
    }
}