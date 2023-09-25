using UnityEngine;

[System.Serializable]
public class EntityStats
{
    public float damageMultiplier = 1.0f;
    public float criticalChance = 0.0f;
    public float criticalDamageMultiplier = 2.0f;
    public float healMultiplier = 1.0f;
    public float armorMultiplier = 1.0f;
    public int turnSpeedBonus = 0;

    public int GetDamage(int baseDamage)
    {
        float damage = baseDamage;
        damage *= damageMultiplier;

        if (criticalChance > Random.value)
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
        return Mathf.Min(baseTurnSpeed - turnSpeedBonus, 1);
    }
}
