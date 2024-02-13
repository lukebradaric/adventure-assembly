using AdventureAssembly.Units.Abilities;
using AdventureAssembly.Units.Characters;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class Hero : Character
    {
        new public HeroStats Stats => (HeroStats)base.Stats;
        public HeroData HeroData { get; set; }

        [OdinSerialize] public List<Ability> Abilities { get; protected set; } = new List<Ability>();

        /// <summary>
        /// Did this unit die due to a hazard collision?
        /// </summary>
        public bool CollisionDeath { get; set; } = false;

        /// <summary>
        /// Initializes this unit at position and registers all abilities.
        /// </summary>
        /// <param name="unitData">The HeroData of this hero</param>
        /// <param name="position">The starting position of this unit</param>
        public override void Initialize(CharacterData unitData, Vector2Int position)
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

        /// <summary>
        /// Adds a rigidbody to this Hero for the death animation.
        /// </summary>
        protected override void OnDie()
        {
            base.OnDie();

            Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            rigidbody.velocity = new Vector2(Random.Range(-10, 10), Random.Range(10, 20));
            rigidbody.angularVelocity = Random.Range(300, 600);
            rigidbody.gravityScale = 7f;
            transform.SetParent(null);
        }

        /// <summary>
        /// Removes the unit from the UnitManager and destroys after 3 seconds.
        /// </summary>
        public override void Destroy()
        {
            GridManager.RemoveUnit(this);
            Destroy(gameObject, 3f);
        }

        /// Updates all of the abilities on this Hero.
        /// </summary>
        /// <param name="time">The time since the last update</param>
        public override void OnUpdate(float time)
        {
            foreach (Ability ability in Abilities)
            {
                ability.OnUpdate(time);
            }
        }
    }
}