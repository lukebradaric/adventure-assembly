using AdventureAssembly.Core;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public class Hero : Unit
    {
        public HeroData HeroData { get; set; }

        public override void Initialize(UnitData unitData, Vector2Int position)
        {
            base.Initialize(unitData, position);

            this.HeroData = (HeroData)unitData;
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
            base.Move(direction);

            // Move to new position
            transform.DOMove((Vector2)Position, TickManager.Instance.TickInterval).SetEase(Ease.OutCubic);
        }
    }
}