using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Projectile _projectileStats;

    [SerializeField] private List<Sprite> _sprites;

    [Space]
    [Header("Numbers")]
    [SerializeField] private float _animInterval;

    private int _spriteIndex;

    private void Awake()
    {
        InvokeRepeating(nameof(UpdateFrames), 0, _animInterval);
    }

    private void UpdateFrames()
    {
        if (_spriteIndex >= _sprites.Count)
        {
            _spriteIndex = 0;
            _spriteRenderer.sprite = _sprites[_spriteIndex];
        }
        else
        {
            _spriteIndex++;
            _spriteRenderer.sprite = _sprites[_spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable hit))
        {
            hit.TakeDamage(_projectileStats.damage);
        }
    }
}
