using TinyTools.ScriptableSounds;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] protected GameObject _destroyParticlesPrefab;
    [SerializeField] protected ScriptableSound _destroySound;

    [Space]
    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] protected int _damage;
    public int Damage => _damage;
    [SerializeField] private float _maxLifetime = 5f;

    protected Entity _target;
    private Vector2 _moveDirection;

    protected virtual void Awake()
    {
        Destroy(gameObject, _maxLifetime);
    }

    protected virtual void FixedUpdate()
    {
        // If target exists, update move direction
        if (_target != null)
        {
            _moveDirection = (_target.transform.position - gameObject.transform.position).normalized;
        }

        gameObject.transform.up = _moveDirection;
        _rigidbody.velocity = _moveDirection * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("colliding with something");
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            OnEnemyCollision(enemy);
        }
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void SetTarget(Entity target)
    {
        _target = target;
        _moveDirection = (_target.transform.position - gameObject.transform.position).normalized;
    }

    protected virtual void OnEnemyCollision(Enemy enemy)
    {
        enemy.Damage(_damage);

        if (_destroyParticlesPrefab != null)
        {
            Instantiate(_destroyParticlesPrefab, transform.position, Quaternion.identity);
        }

        _destroySound?.Play();

        Destroy(gameObject);
    }
}
