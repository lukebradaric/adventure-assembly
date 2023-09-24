using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [PropertySpace]
    [Title("Character Settings")]
    [OdinSerialize] public Color Color { get; private set; } = Color.magenta;

    [PropertySpace]
    [Title("Character Class")]
    [OdinSerialize] public List<Class> Classes { get; private set; } = new List<Class>();

    protected override void OnEnable()
    {
        base.OnEnable();
        CharacterManager.Register(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CharacterManager.Unregister(this);
    }

    public override void Die()
    {
        base.Die();

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        rigidbody.velocity = new Vector2(Random.Range(-10, 10), Random.Range(10, 20));
        rigidbody.angularVelocity = Random.Range(300, 600);
        rigidbody.gravityScale = 7f;

        CharacterManager.Unregister(this);
        Destroy(gameObject, 3f);
    }
}
