using AdventureAssembly.Units.Heroes;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    public class HeroSelectionStart : MonoBehaviour
    {
        public List<HeroData> heroData = new List<HeroData>();

        private void Start()
        {
            InterfaceManager.Instance.HeroSelectionInterface.Show(heroData);
        }
    }
}