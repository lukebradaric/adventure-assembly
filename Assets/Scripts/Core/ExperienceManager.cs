using TinyTools.Generics;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Core
{
    /// <summary>
    /// Manager for keeping track of player experience and when to spawn to hero chests.
    /// </summary>
    public class ExperienceManager : Singleton<ExperienceManager>
    {
        [Space]
        [Header("Events")]
        [SerializeField] private VoidScriptableEvent _openHeroChestScriptableEvent;

        [Space]
        [Header("Debugging")]
        [SerializeField] private int _currentExperience = 0;
        [SerializeField] private int currentLevel = 0;

        public void AddExperience(int experience)
        {
            _currentExperience++;
            UpdateLevel();
        }

        public void UpdateLevel()
        {
            int newLevel = (int)Mathf.Sqrt(_currentExperience);

            if (newLevel != currentLevel)
            {
                currentLevel = newLevel;
                _openHeroChestScriptableEvent?.Raise();
            }
        }
    }
}