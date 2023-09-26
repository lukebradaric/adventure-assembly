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

    private void OnEnable()
    {
        foreach (ClassModifier modifier in modifiers)
        {
            modifier.SetClass(this);
        }
    }

    public void Register()
    {
        foreach (ClassModifier modifier in modifiers)
        {
            modifier.RegisterCharacter();
        }
    }

    public void Unregister()
    {
        foreach (ClassModifier modifier in modifiers)
        {
            modifier.UnregisterCharacter();
        }
    }
}
