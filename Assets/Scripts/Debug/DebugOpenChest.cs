using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Debug
{
    public class DebugOpenChest : DebugKeyPress
    {
        [SerializeField] private GameScriptableEvent _onHeroSelection;

        public override void DebugKeyPressed()
        {
            _onHeroSelection?.Invoke();
        }
    }
}