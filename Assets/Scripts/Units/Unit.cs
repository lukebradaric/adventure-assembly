using AdventureAssembly.Core;
using AdventureAssembly.Core.Extensions;
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

        public Vector2Int Position { get; protected set; }
        public Vector2Int LastPosition { get; protected set; }

        public event Action<Unit> Died;

        public abstract void Move(Vector2Int direction);

        public virtual void Die()
        {
            Died?.Invoke(this);
        }

        public void Move(Direction direction)
        {
            Move(direction.ToVector2Int());
        }

        public void FlipSprite(int x)
        {
            if (x == 0)
            {
                return;
            }

            Vector3 localScale = transform.localScale;
            //Vector3 rotation = transform.eulerAngles;

            if (x > 0)
            {
                //transform.DORotate(new Vector3(rotation.x, 0, rotation.z), TurnManager.Instance.TurnInterval / 2f);
                localScale.x = Mathf.Abs(localScale.x);
            }
            else if (x < 0)
            {
                //transform.DORotate(new Vector3(rotation.x, 180, rotation.z), TurnManager.Instance.TurnInterval / 2f);
                localScale.x = -Mathf.Abs(localScale.x);
            }

            transform.localScale = localScale;
        }
    }
}