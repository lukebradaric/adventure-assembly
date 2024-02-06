using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units.Modifiers
{
    /// <summary>
    /// EnemyModifier that will spawn gold whenever an enemy takes damage.
    /// </summary>
    public class GoldenEnemyModifier : CharacterModifier
    {
        public override void Apply(Character character)
        {
            character.Damaged += OnDamaged;
        }

        public override void Remove(Character character)
        {
            character.Damaged -= OnDamaged;
        }

        private void OnDamaged(DamageData data)
        {
            GoldManager.Instance.AddGold(data.Target.transform.position, data.Value);
        }
    }
}