using AdventureAssembly.Interface;
using AdventureAssembly.Units.Heroes;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Debug
{
    public class DebugOpenChest : DebugKeyPress
    {
        [Space]
        [SerializeField] private bool _specificHeroData = false;

        [HideIf(nameof(_specificHeroData))]
        [SerializeField] private HeroDataListScriptableVariable _heroDataList;

        [ShowIf(nameof(_specificHeroData))]
        [SerializeField] private List<HeroData> _heroData = new List<HeroData>();

        public override void DebugKeyPressed()
        {
            if (_specificHeroData)
            {
                InterfaceManager.Instance.HeroSelectionInterface.Show(_heroData);
            }
            else
            {
                InterfaceManager.Instance.HeroSelectionInterface.Show(_heroDataList.GetRandom(3));
            }
        }
    }
}