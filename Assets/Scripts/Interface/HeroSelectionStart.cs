using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class HeroSelectionStart : MonoBehaviour
    {
        [SerializeField] private GameScriptableEvent _onHeroSelection;

        private void Start()
        {
            _onHeroSelection?.Invoke();
        }
    }
}