using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units
{
    [System.Serializable]
    public class Stat<T>
    {
        // TODO: OnChanged events

        [OdinSerialize] public T BaseValue { get; private set; }

        public T Value
        {
            get
            {
                T value = BaseValue;

                foreach (var modifier in _statModifiers)
                {
                    if (modifier == null) continue;

                    value = modifier.Process(value);
                }

                return value;
            }
        }

        private List<StatModifier<T>> _statModifiers = new List<StatModifier<T>>();

        public Stat(T value = default)
        {
            BaseValue = value;
        }

        public void AddModifier(StatModifier<T> modifier)
        {
            _statModifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier<T> modifier)
        {
            if (_statModifiers.Contains(modifier))
            {
                Debug.LogError("Modifier was not found on stat!");
                return;
            }

            _statModifiers.Remove(modifier);
        }
    }
}