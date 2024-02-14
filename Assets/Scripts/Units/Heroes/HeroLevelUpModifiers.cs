using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.ScriptableEvents;

namespace AdventureAssembly.Units.Heroes
{
    /// <summary>
    /// Adds global modifiers to all heroes when the player levels up.
    /// </summary>
    public class HeroLevelUpModifiers : SerializedMonoBehaviour
    {
        [OdinSerialize] public List<GlobalCharacterStatModifier> Modifiers { get; protected set; } = new List<GlobalCharacterStatModifier>();

        public void OnPlayerLeveledUp(GameEventData gameEventData)
        {
            foreach (var modifier in Modifiers)
            {
                HeroManager.Instance.AddGlobalModifier(modifier);
            }
        }
    }
}