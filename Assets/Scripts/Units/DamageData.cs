﻿using AdventureAssembly.Units.Characters;
using UnityEngine;

namespace AdventureAssembly.Units
{
    /// <summary>
    /// A class to hold data about dealing/taking damage.
    /// </summary>
    public class DamageData
    {
        public DamageData(CharacterUnit source, CharacterUnit target, int baseValue)
        {
            Source = source;
            Target = target;
            BaseValue = baseValue;

            source.Stats.GetDamageData(this);
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
        /// The CharacterUnit that is dealing this damage.
        /// </summary>
        public CharacterUnit Source { get; set; }

        /// <summary>
        /// The CharacterUnit that this damage is being dealt to.
        /// </summary>
        public CharacterUnit Target { get; set; }
    }
}