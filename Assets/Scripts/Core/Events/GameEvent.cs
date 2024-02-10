using AdventureAssembly.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Core.Events
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "GameEvent")]
    public class GameEvent : ScriptableObject
    {
        [TextArea(minLines: 5, maxLines: 10)]
        [SerializeField] private string _description;

        // Event fired to listen to event using code
        public Action<Component, object> Event;

        // List of components that are listening
        private List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Invoke()
        {
            Invoke(null, null);
        }

        public void Invoke(object data)
        {
            Invoke(null, data);
        }

        public void Invoke(Component sender)
        {
            Invoke(sender, null);
        }

        public void Invoke(Component sender, object data)
        {
            // Loop backwards over event listeners
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i]?.OnInvoked(sender, data);
            }

            Event?.Invoke(sender, data);
        }

        public void RegisterListener(GameEventListener gameEventListener)
        {
            if (_listeners.Contains(gameEventListener))
            {
                UnityEngine.Debug.LogError($"GameEvent {this.name} already contains {gameEventListener.name} as a listener!");
                return;
            }

            _listeners.Add(gameEventListener);
        }

        public void UnregisterListener(GameEventListener gameEventListener)
        {
            if (!_listeners.Contains(gameEventListener))
            {
                UnityEngine.Debug.LogError($"GameEvent {this.name} could not find {gameEventListener.name} as a listener!");
                return;
            }

            _listeners.Remove(gameEventListener);
        }
    }
}