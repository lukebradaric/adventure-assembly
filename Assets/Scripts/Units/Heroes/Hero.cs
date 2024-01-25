using AdventureAssembly.Units.Abilities;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class Hero : Unit
    {
        new public HeroStats Stats => (HeroStats)this.Stats;

        public HeroData HeroData { get; set; }

        [OdinSerialize] public List<Ability> Abilities { get; protected set; } = new List<Ability>();

        public override void Initialize(UnitData unitData, Vector2Int position)
        {
            base.Initialize(unitData, position);
            this.HeroData = (HeroData)unitData;

            // Clone abilities and register to this hero
            foreach (Ability ability in HeroData.Abilities)
            {
                Ability newAbility = ability.GetClone();
                newAbility.Initialize(this);
                Abilities.Add(newAbility);
            }
        }

        public override void Die()
        {
            base.Die();

            Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            rigidbody.velocity = new Vector2(Random.Range(-10, 10), Random.Range(10, 20));
            rigidbody.angularVelocity = Random.Range(300, 600);
            rigidbody.gravityScale = 7f;
            transform.SetParent(null);

            Destroy(gameObject, 3f);
        }

        public override void OnTick()
        {
            foreach (Ability ability in Abilities)
            {
                ability.OnTick();
            }
        }
    }
}