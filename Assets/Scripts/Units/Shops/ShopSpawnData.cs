using AdventureAssembly.Core;
using AdventureAssembly.Core.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Shops
{
    internal struct ShopDataWeighted
    {
        [Tooltip("What is the weighted chance that this shop should spawn?")]
        public int Weight;

        [Tooltip("What shop should spawn?")]
        public ShopData ShopData;
    }

    internal struct ShopSpawnEntry
    {
        [Tooltip("At what time should one of the shops in the list spawn? Measured in seconds.")]
        public float Time;
        [Tooltip("What shops should be able to spawn at this time?")]
        public List<ShopDataWeighted> Shops;
    }

    internal struct LuckShopSpawnEntry
    {
        [Tooltip("At what time should this shop have a chance to spawn? Measured in seconds.")]
        public float Time;
        [Tooltip("What is the chance that this shop will spawn at the specified time? (0.0 - 1.0)")]
        public float Chance;
        [Tooltip("What shop should spawn?")]
        public ShopData ShopData;
    }

    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ShopSpawnData")]
    public class ShopSpawnData : SerializedScriptableObject
    {
        [Tooltip("What shops should be guaranteed to spawn at certain times?")]
        [OdinSerialize] private List<ShopSpawnEntry> ShopSpawns = new List<ShopSpawnEntry>();

        [Tooltip("What shops should have a chance to spawn at certain times?")]
        [OdinSerialize] private List<LuckShopSpawnEntry> LuckShopSpawns = new List<LuckShopSpawnEntry>();

        /// <summary>
        /// Returns a shop to spawn if one should spawn at the given time. Returns null if none should spawn.
        /// </summary>
        /// <param name="currentTime">The current time in the level. Measured in seconds.</param>
        /// <returns></returns>
        public ShopData GetShopToSpawn(int currentTime)
        {
            // Check each timed spawn
            foreach (ShopSpawnEntry shopSpawnEntry in ShopSpawns)
            {
                // If there is an entry with a matching time, find a shop using weight
                if (currentTime == shopSpawnEntry.Time)
                {
                    WeightedList<ShopData> shops = new WeightedList<ShopData>();
                    foreach (ShopDataWeighted shopDataWeighted in shopSpawnEntry.Shops)
                    {
                        shops.Add(shopDataWeighted.ShopData, shopDataWeighted.Weight);
                    }

                    if (shops.Count == 0)
                    {
                        Debug.LogError("There were no valid shop weights found in ShopSpawnEntry!");
                        return null;
                    }

                    return shops.Random();
                }
            }

            // Check each luck spawn and see if chance is hit
            foreach (LuckShopSpawnEntry luckSpawnEntry in LuckShopSpawns)
            {
                if (currentTime == luckSpawnEntry.Time && luckSpawnEntry.Chance.Chance())
                {
                    return luckSpawnEntry.ShopData;
                }
            }

            return null;
        }
    }
}