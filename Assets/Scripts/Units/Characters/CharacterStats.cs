using AdventureAssembly.Units.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public class CharacterStats : SerializedMonoBehaviour
    {
        public Character Character { get; protected set; }
        public CharacterData CharacterData { get; protected set; }

        public virtual void Initialize(Character unit)
        {
            this.Character = unit;
            this.CharacterData = unit.CharacterData;
        }

        public Stat<float> DamageMultiplier { get; set; } = new Stat<float>(1f);
        public Stat<float> DamageBonus { get; set; } = new Stat<float>(0f);
        public Stat<float> MaxHealthMultiplier { get; set; } = new Stat<float>(1f);
        public Stat<float> MaxHealthBonus { get; set; } = new Stat<float>(0f);
        public Stat<float> HealMultiplier { get; set; } = new Stat<float>(0f);
        public Stat<float> HealBonus { get; set; } = new Stat<float>(0f);

        public virtual DamageData GetDamageData(DamageData damageData)
        {
            float damage = damageData.BaseValue;

            // Add damage bonus
            damage += DamageBonus.Value;

            // Multiply by damage multiplier
            damage *= DamageMultiplier.Value;

            // Ceil damage to integer
            damageData.Value = (int)Mathf.Ceil(damage);

            return damageData;
        }

        public virtual HealData GetHealData(HealData healData)
        {
            float heal = healData.BaseValue;

            heal += HealBonus.Value;

            heal *= HealMultiplier.Value;

            healData.Value = (int)Mathf.Ceil(heal);

            return healData;
        }

        public virtual int GetMaxHealth()
        {
            return (int)Mathf.Ceil(CharacterData.MaxHealth * MaxHealthMultiplier.Value) + (int)MaxHealthBonus.Value;
        }
    }
}