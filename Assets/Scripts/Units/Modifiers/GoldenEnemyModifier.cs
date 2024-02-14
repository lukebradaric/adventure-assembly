using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    /// <summary>
    /// EnemyModifier that will spawn gold whenever an enemy takes damage.
    /// </summary>
    public class GoldenEnemyModifier : CharacterModifier
    {
        [SerializeField] private float _goldPerHealthLost = 0.2f;

        public override void ApplyToCharacter(Character character)
        {
            character.Damaged += OnDamaged;
        }

        public override void RemoveFromCharacter(Character character)
        {
            character.Damaged -= OnDamaged;
        }

        private void OnDamaged(DamageData data)
        {
            // Gold earned = Max between damaged taken and current health times goldperhealth lost
            int gold = (int)Mathf.Round(Mathf.Min(data.Value, data.Target.CurrentHealth) * _goldPerHealthLost);
            GoldManager.Instance.AddGold(data.Target.transform.position, gold);
        }
    }
}