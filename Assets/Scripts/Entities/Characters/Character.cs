using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [PropertySpace]
    [Title("Class")]
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

    protected override void Die()
    {
        base.Die();
        Debug.Log("Character died.");
        CharacterManager.Unregister(this);
    }
}
