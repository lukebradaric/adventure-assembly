using AdventureAssembly.Units.Interactables;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Debug
{
    public class DebugOpenShop : DebugKeyPress
    {
        [SerializeField] private GameScriptableEvent _onShopOpened;
        [SerializeField] private ShopData _shopData;

        public override void DebugKeyPressed()
        {
            _onShopOpened?.Invoke(null, _shopData);
        }
    }
}