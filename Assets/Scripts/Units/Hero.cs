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

            Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            rigidbody.velocity = new Vector2(Random.Range(-10, 10), Random.Range(10, 20));
            rigidbody.angularVelocity = Random.Range(300, 600);
            rigidbody.gravityScale = 7f;
            transform.SetParent(null);

            Destroy(gameObject, 3f);
        }

        public override void Move(Vector2Int direction)
        {
            // Save last position
            LastPosition = Position;

            // Update new position
            Position += direction;

            this.FlipSprite(direction.x);

            // Move to new position
            transform.DOMove((Vector2)Position, TickManager.Instance.TickInterval).SetEase(Ease.OutCubic);
        }
    }
}