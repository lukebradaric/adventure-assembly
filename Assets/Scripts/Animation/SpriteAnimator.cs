using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

    [Space]
    [Header("Settings")]
    [SerializeField] private float _animationInterval;

    private int _spriteIndex = 0;

    private void Awake()
    {
        InvokeRepeating(nameof(UpdateFrames), 0, _animationInterval);
    }

    private void OnDestroy()
    {
        CancelInvoke();
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
