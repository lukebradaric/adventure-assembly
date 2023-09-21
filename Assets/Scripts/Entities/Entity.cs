using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : SerializedMonoBehaviour
{
    [PropertySpace]
    [Title("Components")]
    [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }

    [PropertySpace]
    [Title("Settings")]
    [OdinSerialize] public string Name { get; private set; }
    [OdinSerialize] public int BaseHealth { get; private set; }

    [PropertySpace]
    [Title("Animation")]
    [OdinSerialize] public EntityTween MoveTween { get; private set; } = new BasicEntityTween();

    [PropertySpace]
    [Title("Abilities")]
    [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();

    public int CurrentHealth { get; private set; }
    public Vector2Int Position { get; private set; }

    public bool CanMove { get; set; } = true;

    public virtual void Initialize()
    {
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        foreach (Ability ability in Abilities)
        {
            ability.Initialize(this);
        }
    }

    public void Turn()
    {
        foreach (Ability ability in Abilities)
        {
            ability.Turn();
        }
    }

    public void Move(Vector2Int movement)
    {
        if (!CanMove)
        {
            return;
        }

        Position += movement;
        Flip(movement.x);

        MoveTween.Animate(this, Position, TurnManager.TurnInterval);
    }

    protected void Flip(int x)
    {
        if (x == 0)
        {
            return;
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(x) * 1;
        transform.localScale = scale;
    }
}