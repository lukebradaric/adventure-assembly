using AdventureAssembly.Units.Heroes;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    [System.Serializable]
    public abstract class Ability
    {
        [Tooltip("How often should this ability occur?")]
        [SerializeField] private int _ticks;

        // Current ticks until ability is executed
        protected int _currentTicks;

        // The hero this ability is on
        protected Hero _hero;

        public void Initialize(Hero hero)
        {
            _hero = hero;
            _currentTicks = _ticks;
        }

        public void OnTick()
        {
            _currentTicks--;

            if (_currentTicks <= 0)
            {
                Execute();

                // TODO: Calculate current ticks based on Hero stats
                _currentTicks = _ticks;
            }
        }

        protected abstract void Execute();

        public Ability GetClone()
        {
            return (Ability)this.MemberwiseClone();
        }
    }
}