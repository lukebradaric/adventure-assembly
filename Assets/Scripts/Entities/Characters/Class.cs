using UnityEngine;

[CreateAssetMenu(menuName = "Class")]
public class Class : ScriptableObject
{
    public Color color;
    [TextArea]
    public string description;
}
