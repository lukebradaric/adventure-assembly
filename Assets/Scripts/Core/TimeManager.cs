using UnityEngine;

namespace AdventureAssembly.Core
{
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