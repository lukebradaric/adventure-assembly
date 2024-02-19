using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units.Shops
{
    public class ShopSpawner : MonoBehaviour
    {
        [BoxGroup("Components")]
        [SerializeField] private ShopSpawnData _shopSpawnData;

        private void OnEnable()
        {
            TimeManager.CurrentTimeUpdated += OnCurrentTimeUpdate;
        }

        private void OnDisable()
        {
            TimeManager.CurrentTimeUpdated -= OnCurrentTimeUpdate;
        }

        private void OnCurrentTimeUpdate(int currentTime)
        {
            TrySpawnShop(currentTime);
        }

        private void TrySpawnShop(int currentTime)
        {
            ShopData shopData = _shopSpawnData.GetShopToSpawn(currentTime);

            if (shopData == null)
            {
                return;
            }

            shopData.Create(GridManager.GetRandomEmptyPlayerMapPosition());
        }
    }
}