using UnityEngine;

namespace AdventureAssembly.Core
{
    /// <summary>
    /// Manager for handling pausing/unpausing the game.
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
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