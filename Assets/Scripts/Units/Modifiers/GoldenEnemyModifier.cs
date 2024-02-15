using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    /// <summary>
    /// EnemyModifier that will spawn gold whenever an enemy takes damage.
    /// </summary>
    public class GoldenEnemyModifier : CharacterModifier
    {
        [BoxGroup("Settings")]
        [SerializeField] private float _goldPerHealthLost = 0.2f;

        protected override void OnApplyToCharacter(Character character)
        {
            character.Damaged += OnDamaged;
        }

        protected override void OnRemoveFromCharacter(Character character)
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