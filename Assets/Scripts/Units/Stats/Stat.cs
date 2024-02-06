using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Stats
{
    /// <summary>
    /// A generic stat value class that can have processes applied to it. Processes affect the calculated value
    /// </summary>
    /// <typeparam name="T">The generic value</typeparam>
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

                foreach (var modifier in _statProcesses)
                {
                    if (modifier == null) continue;

                    value = modifier.Process(value);
                }

                return value;
            }
        }

        private List<StatProcess<T>> _statProcesses = new List<StatProcess<T>>();

        public Stat(T value = default)
        {
            BaseValue = value;
        }

        public void AddProcess(StatProcess<T> modifier)
        {
            _statProcesses.Add(modifier);
        }

        public void AddProcess(StatProcess statProcess)
        {
            AddProcess((StatProcess<T>)statProcess);
        }

        public void RemoveProcess(StatProcess<T> modifier)
        {
            if (!_statProcesses.Contains(modifier))
            {
                Debug.LogError($"Modifier was not found on stat! {modifier.GetType().Name}:{modifier.GetHashCode()}");
                return;
            }

            _statProcesses.Remove(modifier);
        }

        public void RemoveProcess(StatProcess statProcess)
        {
            RemoveProcess((StatProcess<T>)statProcess);
        }
    }
}