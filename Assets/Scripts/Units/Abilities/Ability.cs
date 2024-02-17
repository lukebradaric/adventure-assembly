using AdventureAssembly.Core;
using AdventureAssembly.Units.Heroes;
using System.Collections;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public abstract class Ability : CloneObject<Ability>
    {
        private const float MinTimeBetweenExecute = 0.1f;

        // Current ticks until ability is executed
        protected float _currentTime;

        // The hero this ability is on
        protected Hero _hero;

        public virtual void Initialize(Hero hero)
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
            int bonusCount = _hero.Stats.GetAbilityExecuteCount() - 1;
            if (bonusCount > 0)
            {
                CoroutineManager.Instance.StartCoroutine(ExecuteCoroutine(bonusCount));
            }

            // Reset ability time
            _currentTime = _hero.Stats.GetAbilitySpeed();
        }

        protected virtual void OnExecute()
        {
            // If the ability successfully executed, play audio
            if (Execute())
            {
                _hero.HeroData.AbilitySound?.Play();
            }
        }

        protected abstract bool Execute();

        private IEnumerator ExecuteCoroutine(int bonusCount)
        {
            for (int i = 0; i < bonusCount; i++)
            {
                yield return new WaitForSeconds(MinTimeBetweenExecute);
                OnExecute();
            }
        }

        public virtual void OnDrawGizmos() { }
    }
}