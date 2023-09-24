using UnityEngine;

[CreateAssetMenu(menuName = "Stat")]
public class Stat : ScriptableObject
{
    [HideInInspector] public float value;

    public float GetValue()
    {
        return StatManager.GetValue(this);
    }

    public void AddValue(float value)
    {
        StatManager.AddValue(this, value);
    }
}
