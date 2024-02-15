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

        private bool _isRemoved = false;

        protected abstract void OnApplyToCharacter(Character character);
        protected abstract void OnRemoveFromCharacter(Character character);

        public void ApplyToCharacter(Character character)
        {
            // If this is not a global modifier and is temporary, remove after duration
            if (this is not GlobalCharacterStatModifier)
            {
                CoroutineManager.Instance.StartCoroutine(RemoveCoroutine(character, Duration));
            }

            OnApplyToCharacter(character);
        }

        public void RemoveFromCharacter(Character character)
        {
            _isRemoved = true;
            OnRemoveFromCharacter(character);
        }

        private IEnumerator RemoveCoroutine(Character character, float duration)
        {
            yield return new WaitForSeconds(duration);

            if (_isRemoved)
            {
                yield break;
            }

            this.RemoveFromCharacter(character);
        }
    }
}