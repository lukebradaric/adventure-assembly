using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class Enemy : Entity
{
    [PropertySpace]
    [Title("Enemy Settings")]
    [OdinSerialize] public float KillExperience { get; private set; } = 1;

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
    }
}
