using AdventureAssembly.Core;
using UnityEngine;

namespace AdventureAssembly.Units.Stats
{
    /// <summary>
    /// Increases the value of a float stat by a percentage per gold the player has.
    /// </summary>
    public class FloatMultiplierPerGoldStatProcess : StatProcess<float>
    {
        [SerializeField] private float _damageMultiplierPerGold = 0.01f;

        public override float Process(float value)
        {
            return value * (1 + (GoldManager.Instance.GoldCount * _damageMultiplierPerGold));
        }
    }
}