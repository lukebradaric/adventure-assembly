using AdventureAssembly.Core;
using System.Collections.Generic;
using TinyTools.Extensions;
using TinyTools.ScriptableVariables;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    /// <summary>
    /// A ScriptableObject containing a list of HeroData. Offers functions for retrieving random/specific HeroData.
    /// </summary>
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ScriptableVariables/HeroDataListScriptableVariable")]
    public class HeroDataListScriptableVariable : ScriptableVariable<List<HeroData>>
    {
        public HeroData GetRandom()
        {
            return _value.Random();
        }

        public List<HeroData> GetRandom(int count, bool allowDuplicate = false)
        {
            if (count == 0)
            {
                Debug.LogWarning("Trying to get list of 0 elements.");
            }

            List<HeroData> heroData = new List<HeroData>();

            if (allowDuplicate)
            {
                for (int i = 0; i < count; i++)
                {
                    heroData.Add(_value.Random());
                }
            }
            else
            {
                List<HeroData> tempList = new List<HeroData>(_value);
                for (int i = 0; i < count; i++)
                {
                    heroData.Add(tempList.RemoveRandom());
                }
            }

            return heroData;
        }
    }
}