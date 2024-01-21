using System;
using System.Collections;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Core
{
    public class TurnManager : Singleton<TurnManager>
    {
        public static event Action TurnUpdate;

        // TODO: Convert to serialized property
        [SerializeField] private float _turnInterval = 0.35f;
        public float TurnInterval
        {
            get
            {
                return _turnInterval;
            }
        }

        private Coroutine _turnCoroutine;

        private void Start()
        {
            Debug.Log("Starting turn updates");
            StartTurnUpdates();
        }

        public void StartTurnUpdates()
        {
            StartCoroutine(TurnCoroutine());
        }

        public void StopTurnUpdates()
        {
            if (_turnCoroutine != null)
            {
                StopCoroutine(_turnCoroutine);
            }

            _turnCoroutine = null;
        }

        private IEnumerator TurnCoroutine()
        {
            yield return new WaitForSeconds(_turnInterval);
            TurnUpdate?.Invoke();
            _turnCoroutine = StartCoroutine(TurnCoroutine());
        }
    }
}