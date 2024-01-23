using Sirenix.Serialization;
using System;
using System.Collections;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Core
{
    public class TickManager : Singleton<TickManager>
    {
        public static event Action TickUpdate;
        public static event Action TickLateUpdate;

        [OdinSerialize] public float TickInterval { get; private set; } = 0.33333f;

        private Coroutine _tickCoroutine;

        private void Start()
        {
            StartTickUpdates();
        }

        public void StartTickUpdates()
        {
            StartCoroutine(TickCoroutine());
        }

        public void StopTickUpdates()
        {
            if (_tickCoroutine != null)
            {
                StopCoroutine(_tickCoroutine);
            }

            _tickCoroutine = null;
        }

        private IEnumerator TickCoroutine()
        {
            yield return new WaitForSeconds(TickInterval);
            TickUpdate?.Invoke();
            _tickCoroutine = StartCoroutine(TickCoroutine());
            TickLateUpdate?.Invoke();
        }
    }
}