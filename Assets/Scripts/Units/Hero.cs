using AdventureAssembly.Core;
using DG.Tweening;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Hero : Unit
    {
        [OdinSerialize] public HeroData HeroData { get; set; }

        public void Initialize(HeroData heroData, Vector2Int position)
        {
            this.HeroData = heroData;
            this.Position = position;

            SpriteRenderer.sprite = HeroData.Sprite;
            name = $"{HeroData.Name}";
        }

        public override void Die()
        {
            base.Die();

            Destroy(gameObject);
        }

        public override void Move(Vector2Int direction)
        {
            // Save last position
            LastPosition = Position;

            // Update new position
            Position += direction;

            this.FlipSprite(direction.x);

            // Move to new position
            transform.DOMove((Vector2)Position, TurnManager.Instance.TurnInterval).SetEase(Ease.OutCubic);
        }
    }
}