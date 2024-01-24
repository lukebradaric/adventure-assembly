using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class HeroStats : MonoBehaviour
    {
        public Stat<float> DamageMultiplier { get; set; } = new Stat<float>(1);

        public DamageData GetDamageData(DamageData damageData)
        {
            float damage = damageData.Value;
            damage *= DamageMultiplier.Value;

            damageData.Value = (int)Mathf.Ceil(damage);
            return damageData;
        }
    }
}