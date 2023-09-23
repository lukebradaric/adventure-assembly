using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricChain : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private LineRenderer _electricLine;
    [SerializeField] private Rigidbody2D _rigidbody;

    [Space]
    [Header("Stats")]
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;

    private Entity _target;
    private Vector2 _moveDirection;
    private List<Enemy> _enemiesHit = new List<Enemy>();
    private void FixedUpdate()
    {
        // If target exists, update move direction
        if (_target != null)
        {
            _moveDirection = (_target.transform.position - gameObject.transform.position).normalized;
        }

        //gameObject.transform.up = _moveDirection;
        _rigidbody.velocity = _moveDirection * _speed;
    }
    public void SetTarget(Entity target)
    {
        _target = target;
        _moveDirection = (_target.transform.position - gameObject.transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            OnEnemyCollision(enemy);
        }
    }
    protected virtual void OnEnemyCollision(Enemy enemy)
    {
        
    }
    private void DrawLightning()
    {

    }
}
