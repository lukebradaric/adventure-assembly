﻿using AdventureAssembly.Units.Stats;
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
        public Stat<float> MaxHealthMultiplier { get; set; } = new Stat<float>(1f);
        public Stat<float> CriticalChance { get; set; } = new Stat<float>(0f);
        public Stat<float> CriticalMultiplier { get; set; } = new Stat<float>(2f);

        public virtual DamageData GetDamageData(DamageData damageData)
        {
            // TODO: Run DamageData through all DamageDataProcesses

            float damage = damageData.BaseValue;
            damage *= DamageMultiplier.Value;

            if (CriticalChance.Value > Random.value)
            {
                damageData.IsCritical = true;
            }

            if (damageData.IsCritical)
            {
                damage *= CriticalMultiplier.Value;
            }

            damageData.Value = (int)Mathf.Ceil(damage);

            return damageData;
        }

        public virtual int GetMaxHealth()
        {
            return (int)Mathf.Ceil(CharacterData.MaxHealth * MaxHealthMultiplier.Value);
        }
    }
}