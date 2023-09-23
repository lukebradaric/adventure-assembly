public class Enemy : Entity
{
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
    }
}
