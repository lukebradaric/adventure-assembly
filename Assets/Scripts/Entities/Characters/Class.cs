using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Class")]
public class Class : ScriptableObject
{
    public Color color;
    [TextArea]
    public string description;

    [Space]
    [Header("Modifiers")]
    public List<ClassModifier> modifiers = new List<ClassModifier>();
}
