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

    public void Register(Character character)
    {
        foreach (ClassModifier modifier in modifiers)
        {
            //modifiers
            // Apply each modifier to character
        }
    }

    public void Unregister(Character character)
    {
        // Unapply each modifier to character
    }
}
