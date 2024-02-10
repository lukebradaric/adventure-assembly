using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureAssembly.Core.Events
{
    public class GameEventListener : MonoBehaviour
    {
        [BoxGroup("Event")]
        [Tooltip("What game event should this listener respond to?")]
        [SerializeField] private GameEvent _gameEvent;

        [BoxGroup("Response")]
        [Tooltip("What should happen when the game event is called?")]
        [SerializeField] private UnityEvent<Component, object> _unityEvent;

        private void OnEnable()
        {
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.UnregisterListener(this);
        }

        public void OnInvoked(Component sender, object data)
        {
            _unityEvent.Invoke(sender, data);
        }
    }
}