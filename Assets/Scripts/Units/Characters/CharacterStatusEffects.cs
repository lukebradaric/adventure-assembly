using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public class CharacterStatusEffects : MonoBehaviour
    {
        private HashSet<StatusEffect> _permanentEffects = new HashSet<StatusEffect>();
        private Dictionary<StatusEffect, Coroutine> _temporaryEffects = new Dictionary<StatusEffect, Coroutine>();

        public bool Contains(StatusEffect statusEffect)
        {
            return _permanentEffects.Contains(statusEffect) || _temporaryEffects.ContainsKey(statusEffect);
        }

        public void Add(StatusEffect statusEffect, float duration = -1)
        {
            if (duration == -1)
            {
                // If already in permanent, return
                if (_permanentEffects.Contains(statusEffect))
                {
                    return;
                }

                _permanentEffects.Add(statusEffect);
                return;
            }

            // If effect already in temporary, restart coroutine with new duration
            if (_temporaryEffects.ContainsKey(statusEffect))
            {
                StopCoroutine(_temporaryEffects[statusEffect]);
                _temporaryEffects[statusEffect] = StartCoroutine(StatusEffectCoroutine(statusEffect, duration));
                return;
            }

            // Add a statuseffect to temporary and add the started coroutine
            _temporaryEffects.Add(statusEffect, StartCoroutine(StatusEffectCoroutine(statusEffect, duration)));
        }

        public void Remove(StatusEffect statusEffect)
        {
            if (_permanentEffects.Contains(statusEffect))
            {
                _permanentEffects.Remove(statusEffect);
                return;
            }

            if (_temporaryEffects.ContainsKey(statusEffect))
            {
                // Stop the current coroutine and remove
                StopCoroutine(_temporaryEffects[statusEffect]);
                _temporaryEffects.Remove(statusEffect);
                return;
            }
        }

        private IEnumerator StatusEffectCoroutine(StatusEffect statusEffect, float duration)
        {
            yield return new WaitForSeconds(duration);
            _temporaryEffects.Remove(statusEffect);
        }
    }
}