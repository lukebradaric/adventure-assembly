using AdventureAssembly.Units.Heroes;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public class PaladinAbility : Ability
    {
        [BoxGroup("Settings")]
        [SerializeField] private float _radius;
        [BoxGroup("Settings")]
        [SerializeField] private int _baseHeal;
        [BoxGroup("Settings")]
        [SerializeField] private CharacterStatModifier _damageBuffModifier;

        protected override bool Execute()
        {
            List<Hero> heroes = HeroManager.Instance.GetUnitsInRadius(_hero.Position, _radius);

            if (heroes.Count == 0)
            {
                return false;
            }

            foreach (Hero hero in heroes)
            {
                // If the target is full health, add a modifier instead
                if (hero.IsFullHealth)
                {
                    hero.AddModifier(_damageBuffModifier);
                    continue;
                }

                // Heal the target
                hero.Heal(new HealData(_hero, hero, _baseHeal));
            }

            return true;
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_hero.transform.position, _radius);
        }
    }
}