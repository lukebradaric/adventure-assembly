﻿using UnityEngine;
using UnityEngine.Events;

namespace AdventureAssembly.Units.Interactables
{
    public class InteractableUnit : Unit
    {
        [Space]
        [Header("Events")]
        [SerializeField] private UnityEvent _interactEvent;

        [Space]
        [Header("Settings")]
        [SerializeField] private bool _destroyOnInteract = true;

        public virtual void OnInteract()
        {
            _interactEvent?.Invoke();

            if (_destroyOnInteract)
            {
                Destroy();
            }
        }
    }
}