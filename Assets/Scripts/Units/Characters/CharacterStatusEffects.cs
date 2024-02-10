using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Characters
{
    public class CharacterStatusEffects : MonoBehaviour
    {
        private HashSet<StatusEffects> _permanentEffects = new HashSet<StatusEffects>();
        private Dictionary<StatusEffects, Coroutine> _temporaryEffects = new Dictionary<StatusEffects, Coroutine>();

        public bool Contains(StatusEffects statusEffect)
        {
            return _permanentEffects.Contains(statusEffect) || _temporaryEffects.ContainsKey(statusEffect);
        }

        public void Add(StatusEffects statusEffect, float duration = -1)
        {
            if (duration == -1)
            {
                _permanentEffects.Add(statusEffect);
                return;
            }

            // Add a statuseffect to temporary and add the started coroutine
            _temporaryEffects.Add(statusEffect, StartCoroutine(StatusEffectCoroutine(statusEffect, duration)));
        }

        public void Remove(StatusEffects statusEffect)
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

        private IEnumerator StatusEffectCoroutine(StatusEffects statusEffect, float duration)
        {
            yield return new WaitForSeconds(duration);
            _temporaryEffects.Remove(statusEffect);
        }
    }
}