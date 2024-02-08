using AdventureAssembly.Core;
using AdventureAssembly.Units.Heroes;
using System.Collections;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    [System.Serializable]
    public abstract class Ability
    {
        private const float MinTimeBetweenExecute = 0.1f;

        // Current ticks until ability is executed
        protected float _currentTime;

        // The hero this ability is on
        protected Hero _hero;

        public void Initialize(Hero hero)
        {
            _hero = hero;
            _currentTime = _hero.Stats.GetAbilitySpeed();
        }

        public void OnUpdate(float time)
        {
            _currentTime -= time;

            if (_currentTime > 0)
            {
                return;
            }

            OnExecute();

            // If bonus execution count is greater than zero, start execute coroutine
            int bonusCount = (int)_hero.Stats.AbilityExecuteBonus.Value;
            if (bonusCount > 0)
            {
                CoroutineManager.Instance.StartCoroutine(ExecuteCoroutine(bonusCount));
            }

            // Reset ability time
            _currentTime = _hero.Stats.GetAbilitySpeed();
        }

        protected virtual void OnExecute()
        {
            Execute();
            _hero.HeroData.AbilitySound?.Play();
        }

        protected abstract void Execute();

        private IEnumerator ExecuteCoroutine(int bonusCount)
        {
            for (int i = 0; i < bonusCount; i++)
            {
                yield return new WaitForSeconds(MinTimeBetweenExecute);
                OnExecute();
            }
        }

        public Ability GetClone()
        {
            return (Ability)this.MemberwiseClone();
        }
    }
}