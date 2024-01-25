using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class UnitStats : SerializedMonoBehaviour
    {
        public Unit Unit { get; protected set; }
        public UnitData UnitData { get; protected set; }

        public virtual void Initialize(Unit unit)
        {
            this.Unit = unit;
            this.UnitData = unit.UnitData;
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
            return (int)Mathf.Ceil(UnitData.MaxHealth * MaxHealthMultiplier.Value);
        }
    }
}