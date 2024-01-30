using AdventureAssembly.Interface;
using AdventureAssembly.Units.Heroes;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Debug
{
    public class DebugOpenChest : DebugKeyPress
    {
        [SerializeField] private List<HeroData> heroData = new List<HeroData>();

        public override void DebugKeyPressed()
        {
            if (heroData.Count > 3)
            {
                UnityEngine.Debug.LogError("Cannot provide HeroSelectionInterface with more than 3 hero data! This is a debugging tool for opening chests.");
                return;
            }

            InterfaceManager.Instance.HeroSelectionInterface.Show(heroData);
        }
    }
}