using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using TinyTools.ScriptableVariables;
using TinyTools.ScriptableEvents;
using System;

public class Enemy : Entity
{
    [PropertySpace]
    [Title("Enemy Settings")]
    [OdinSerialize] public float KillExperience { get; private set; } = 1;

    public static event Action<int> OnDeath;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyManager.Register(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EnemyManager.Unregister(this);
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
        CharacterManager.AddExperience(KillExperience);
        OnDeath?.Invoke(BaseHealth);
    }
}
