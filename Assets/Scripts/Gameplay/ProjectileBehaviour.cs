using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private GameObject _destroyParticlesPrefab;

    [Space]
    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Entity _target;
    private Vector2 _moveDirection;

    private void FixedUpdate()
    {
        if (_target == null)
        {
            return;
        }

        _moveDirection = (_target.transform.position - gameObject.transform.position).normalized;

        gameObject.transform.up = _moveDirection;

        _rigidbody.velocity = _moveDirection * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Damage(_damage);

            if (_destroyParticlesPrefab != null)
            {
                Instantiate(_destroyParticlesPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    public void SetTarget(Entity target)
    {
        _target = target;
    }
}
