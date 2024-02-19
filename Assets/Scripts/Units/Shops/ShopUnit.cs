using AdventureAssembly.Units.Interactables;
using Sirenix.OdinInspector;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units.Shops
{
    public class ShopUnit : InteractableUnit
    {
        [BoxGroup("Events")]
        [SerializeField] private GameScriptableEvent _onOpenShop;

        [BoxGroup("Components")]
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public ShopData ShopData { get; protected set; }

        public void Initialize(ShopData shopData, Vector2Int position)
        {
            base.Initialize(position);

            this.ShopData = shopData;
            _spriteRenderer.sprite = ShopData.Sprite;
        }

        public override void OnInteract()
        {
            base.OnInteract();
            _onOpenShop?.Invoke(this, ShopData);
        }
    }
}