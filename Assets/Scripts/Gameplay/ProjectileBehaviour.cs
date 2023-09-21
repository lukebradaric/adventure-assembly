using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Projectile _projectileStats;

    [SerializeField] private GameObject _onContactParticles;

    [SerializeField] private List<Sprite> _sprites;

    [Space]
    [Header("Numbers")]
    [SerializeField] private float _animInterval;

    private int _spriteIndex;
    private GameObject _target;
    private Rigidbody2D _rb;

    private void Awake()
    {
        InvokeRepeating(nameof(UpdateFrames), 0, _animInterval);
        _rb = GetComponent<Rigidbody2D>();
    }

    private void UpdateFrames()
    {
        if (_spriteIndex >= _sprites.Count)
        {
            _spriteIndex = 0;
            _spriteRenderer.sprite = _sprites[_spriteIndex];
            _spriteIndex++;
        }
        else
        {
            _spriteRenderer.sprite = _sprites[_spriteIndex];
            _spriteIndex++;
        }
    }
    private void Update()
    {
        Vector2 moveDirection = _target.transform.position - gameObject.transform.position;
        moveDirection = moveDirection.normalized;
        gameObject.transform.up = moveDirection;
        _rb.MovePosition((Vector2)gameObject.transform.position + moveDirection * Time.deltaTime * _projectileStats.speed); ;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable hit))
        {
            hit.TakeDamage(_projectileStats.damage);
            CancelInvoke();
            //If projectile can explode, spawn explosion. Different Projectiles can have different explosion
            if (_projectileStats.CanExplode)
            {
                //TODO: Add explosion prefab that will deal damage to enemies surrounding the area
            }
            //Spawn Particles and then destroy object after 1 second
            var spawnParticles = Instantiate(_onContactParticles, _target.transform);
            _spriteRenderer.sprite = null;
            Destroy(this.gameObject, 1f);
        }
    }
}
