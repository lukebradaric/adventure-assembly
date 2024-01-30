using System;
using System.Collections.Generic;
using UnityEngine;

namespace TinyTools.ScriptableEvents
{
    public abstract class ScriptableEvent<T> : ScriptableEvent
    {
        public event Action<T> Raised;
        public event Action RaisedVoid;

        public void Raise(T value)
        {
            Raised?.Invoke(value);
            RaisedVoid?.Invoke();

            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Invoke(value);
            }
        }
    }

    public abstract class ScriptableEvent : ScriptableObject
    {
        [Space]
        [Header("Options")]
        [TextArea][SerializeField] private string _description;

        protected readonly List<Action<object>> _listeners = new List<Action<object>>();

        private void OnDisable()
        {
            _listeners.Clear();
        }

        public void AddListener(Action<object> listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(Action<object> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
