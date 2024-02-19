using System;
using System.Collections;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Core
{
    /// <summary>
    /// Manager for handling pausing/unpausing the game.
    /// </summary>
    public class TimeManager : Singleton<TimeManager>
    {
        /// <summary>
        /// The current time of the level in seconds.
        /// </summary>
        public int CurrentTime { get; private set; } = 0;

        public static event Action<int> CurrentTimeUpdated;

        private void Start()
        {
            StartCoroutine(UpdateCoroutine());
        }

        private IEnumerator UpdateCoroutine()
        {
            yield return new WaitForSeconds(1);
            CurrentTime++;
            CurrentTimeUpdated?.Invoke(CurrentTime);
            StartCoroutine(UpdateCoroutine());
        }

        // Pause the game
        public static void Pause()
        {
            Time.timeScale = 0;
        }

        // Unpause the game
        public static void Unpause()
        {
            Time.timeScale = 1;
        }
    }
}