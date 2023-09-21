using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
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

}
