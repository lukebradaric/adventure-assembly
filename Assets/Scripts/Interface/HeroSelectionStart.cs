using AdventureAssembly.Units.Heroes;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class HeroSelectionStart : MonoBehaviour
    {
        [SerializeField] private HeroDataListScriptableVariable _heroDataListScriptableVariable;
        [SerializeField] private int _startingHeroSelectionCount;

        private void Start()
        {
            InterfaceManager.Instance.HeroSelectionInterface.Show(_heroDataListScriptableVariable.GetRandom(_startingHeroSelectionCount));
        }
    }
}