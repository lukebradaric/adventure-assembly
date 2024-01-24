using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using UnityEngine;

namespace AdventureAssembly.Units
{
    public abstract class Unit : SerializedMonoBehaviour
    {
        [PropertySpace]
        [Title("Components")]
        [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }

        public UnitData UnitData { get; private set; }

        public Vector2Int Position { get; protected set; }
        public Vector2Int LastPosition { get; protected set; }

        public event Action<Unit> Died;

        public virtual void Initialize(UnitData unitData, Vector2Int position)
        {
            this.UnitData = unitData;
            this.Position = position;

            SpriteRenderer.sprite = UnitData.Sprite;
            name = $"{UnitData.Name}";
        }

        public virtual void Move(Vector2Int direction)
        {
            // Save last position
            LastPosition = Position;

            // Update new position
            Position += direction;

            // Flip sprite to face movement
            this.FlipSprite(direction.x);
        }

        public virtual void Die()
        {
            Died?.Invoke(this);
        }

        public void FlipSprite(int x)
        {
            if (x == 0)
            {
                return;
            }

            Vector3 localScale = transform.localScale;

            if (x > 0)
                localScale.x = Mathf.Abs(localScale.x);
            else if (x < 0)
                localScale.x = -Mathf.Abs(localScale.x);

            transform.localScale = localScale;
        }
    }
}