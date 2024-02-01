using AdventureAssembly.Units.Characters;
using UnityEngine;

namespace AdventureAssembly.Units
{
    /// <summary>
    /// A class to hold data about dealing/taking damage.
    /// </summary>
    public class DamageData
    {
        public DamageData(int baseValue)
        {
            BaseValue = baseValue;
        }

        /// <summary>
        /// The base value of the damage, before calculations.
        /// </summary>
        public int BaseValue { get; private set; }

        /// <summary>
        /// The calculate value of the damage.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Is this DamageData a critical strike?
        /// </summary>
        public bool IsCritical { get; set; }

        /// <summary>
        /// The direction the damage was dealt in. Usually used for projectiles.
        /// </summary>
        public Vector2 Direction { get; set; } = Vector2.zero;

        /// <summary>
        /// The CharacterUnit that this damage was dealt to.
        /// </summary>
        public CharacterUnit TargetCharacterUnit { get; set; }

        /// <summary>
        /// The CharacterUnit that dealth this damage.
        /// </summary>
        public CharacterUnit SourceCharacterUnit { get; set; }
    }
}