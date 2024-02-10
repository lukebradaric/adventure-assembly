using AdventureAssembly.Units.Characters;

namespace AdventureAssembly.Units
{
    public class HealData
    {
        public HealData(Character source, Character target, int baseValue)
        {
            Source = source;
            Target = target;
            BaseValue = baseValue;

            source.Stats.GetHealData(this);
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
        /// The Character that is dealing this damage.
        /// </summary>
        public Character Source { get; set; }

        /// <summary>
        /// The Character that this damage is being dealt to.
        /// </summary>
        public Character Target { get; set; }
    }
}
