using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float _killExperience = 1;

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
        CharacterManager.AddExperience(_killExperience);
    }
}
