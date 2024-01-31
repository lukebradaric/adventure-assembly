using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public class CharacterUnitStats : SerializedMonoBehaviour
    {
        public CharacterUnit CharacterUnit { get; protected set; }
        public CharacterUnitData CharacterUnitData { get; protected set; }

        public virtual void Initialize(CharacterUnit unit)
        {
            this.CharacterUnit = unit;
            this.CharacterUnitData = unit.CharacterUnitData;
        }

        public Stat<float> DamageMultiplier { get; set; } = new Stat<float>(1f);
        public Stat<float> MaxHealthMultiplier { get; set; } = new Stat<float>(1f);

        public virtual DamageData GetDamageData(DamageData damageData)
        {
            float damage = damageData.BaseValue;
            damage *= DamageMultiplier.Value;

            damageData.Value = (int)Mathf.Ceil(damage);
            return damageData;
        }

        public virtual int GetMaxHealth()
        {
            return (int)Mathf.Ceil(CharacterUnitData.MaxHealth * MaxHealthMultiplier.Value);
        }
    }
}