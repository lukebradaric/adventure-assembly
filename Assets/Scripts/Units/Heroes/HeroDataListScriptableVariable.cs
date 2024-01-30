using AdventureAssembly.Core;
using System.Collections.Generic;
using TinyTools.Extensions;
using TinyTools.ScriptableVariables;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "HeroDataListScriptableVariable")]
    public class HeroDataListScriptableVariable : ScriptableVariable<List<HeroData>>
    {
        public HeroData GetRandom()
        {
            return _value.Random();
        }

        public List<HeroData> GetRandom(int count)
        {
            if(count == 0)
            {
                Debug.LogWarning("Trying to get list of 0 elements.");
            }

            List<HeroData> heroData = new List<HeroData>();
            for (int i = 0; i < count; i++)
            {
                heroData.Add(_value.Random());
            }
            return heroData;
        }
    }
}