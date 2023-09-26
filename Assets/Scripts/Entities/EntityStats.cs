using System.Reflection;
using UnityEngine;

[System.Serializable]
public class EntityStats
{
    public float damageMultiplier = 1.0f;
    public float criticalChance = 0.0f;
    public float criticalDamageMultiplier = 2.0f;
    public float healMultiplier = 1.0f;
    public float armorMultiplier = 1.0f;
    public float areaDamageSizeMultiplier = 1.0f;
    public int turnSpeedBonus = 0;

    public int GetDamage(int baseDamage)
    {
        float damage = baseDamage;
        damage *= damageMultiplier;

        if (criticalChance >= Random.value)
        {
            damage *= criticalDamageMultiplier;
        }

        return (int)Mathf.Ceil(damage);
    }

    public int GetDamageTaken(int baseDamage)
    {
        return (int)(Mathf.Floor(baseDamage / armorMultiplier));
    }

    public int GetHeal(int heal)
    {
        return (int)(Mathf.Ceil(heal * healMultiplier));
    }

    public int GetTurnSpeed(int baseTurnSpeed)
    {
        return Mathf.Max(baseTurnSpeed - turnSpeedBonus, 1);
    }

    public void ChangeValue(string statName, float value)
    {
        FieldInfo fieldInfo = GetType().GetField(statName);

        if (fieldInfo == null)
        {
            Debug.LogError($"Failed to find field of name: {statName}");
            return;
        }

        if (fieldInfo.GetValue(this) is float)
            fieldInfo.SetValue(this, (float)fieldInfo.GetValue(this) + value);
        else if (fieldInfo.GetValue(this) is int)
            fieldInfo.SetValue(this, (int)fieldInfo.GetValue(this) + (int)value);
        else
            Debug.Log("Could not find matching value type to set.");
    }
}
