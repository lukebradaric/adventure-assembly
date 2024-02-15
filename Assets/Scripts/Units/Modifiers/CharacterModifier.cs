using AdventureAssembly.Core;
using AdventureAssembly.Units.Characters;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using UnityEngine;

namespace AdventureAssembly.Units.Modifiers
{
    public abstract class CharacterModifier : CloneObject<CharacterModifier>
    {
        [Tooltip("Is this modifier temporary?")]
        [BoxGroup("General", Order = -1)]
        [OdinSerialize] public bool IsTemporary { get; protected set; } = false;

        [Tooltip("How long should the modifier last for? Measured in seconds.")]
        [BoxGroup("General")]
        [ShowIf(nameof(IsTemporary))]
        [OdinSerialize] public float Duration { get; protected set; } = 1f;

        protected abstract void OnApplyToCharacter(Character character);
        protected abstract void OnRemoveFromCharacter(Character character);

        private Coroutine _removeCoroutine = null;

        ~CharacterModifier()
        {
            if(_removeCoroutine != null )
            {
                CoroutineManager.Instance.StopCoroutine(_removeCoroutine);
                _removeCoroutine = null;
            }
        }

        public void ApplyToCharacter(Character character)
        {
            // If modifier is temporary, start remove coroutine
            if (IsTemporary)
            {
                CoroutineManager.Instance.StartCoroutine(RemoveCoroutine(character));
            }

            OnApplyToCharacter(character);
        }

        public void RemoveFromCharacter(Character character)
        {
            OnRemoveFromCharacter(character);
        }

        private IEnumerator RemoveCoroutine(Character character)
        {
            yield return new WaitForSeconds(Duration);
            RemoveFromCharacter(character);
        }
    }
}