using TinyTools.ScriptableSounds;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _destroyParticlesPrefab;
    [SerializeField] private ScriptableSound _destroySound;

    [Space]
    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Entity _target;
    private Vector2 _moveDirection;

    private void Awake()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
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
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            OnEnemyCollision(enemy);
        }
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
