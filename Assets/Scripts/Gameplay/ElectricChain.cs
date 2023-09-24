using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class ElectricChain : Projectile
{
    [Space]
    [Header("Components")]
    [SerializeField] private LineRenderer _electricLine;
    [SerializeField] private Collider2D _collider;

    [Space]
    [Header("Stats")]
    [SerializeField] private float _radius;

    [Space]
    [Header("Visual Settings")]
    [SerializeField] private float _timeBetweenLineIntervals;
    [SerializeField] private float _maxDeviation;
    [SerializeField] private float _noiseRange;

    [SerializeField] private List<Enemy> _enemiesHit = new List<Enemy>();

    IEnumerator UpdateChainPos(int index, Vector2 position, float time)
    {
        yield return new WaitForSeconds(time);
        _electricLine.positionCount++;
        _electricLine.SetPosition(index, position);
    }
    protected override void OnEnemyCollision(Enemy enemy)
    {
        _enemiesHit = EnemyManager.GetInRadius(enemy.transform.position, _radius);
        _enemiesHit.Add(enemy);
        _enemiesHit = _enemiesHit.Distinct().ToList();
        _electricLine.positionCount = _enemiesHit.Count;
        foreach (Enemy enemyHit in _enemiesHit)
        {
            enemyHit.Damage(_damage);
            Debug.Log("Dealing Damage");
        }
        _collider.enabled = false;
        DrawLightning();
        _destroySound?.Play();
        _spriteRenderer.sprite = null;
        _electricLine.material.DOColor(new Color(1f, 1f, 1f, 0f), TurnManager.TurnInterval);
        Destroy(gameObject, TurnManager.TurnInterval);
    }
    private void DrawLightning()
    {
        for (int i = 0; i < _enemiesHit.Count; i++)
        {
            _electricLine.SetPosition(i, _enemiesHit[i].transform.position);
        }
        if (_destroyParticlesPrefab != null)
        {
            for (int i = 0; i < _enemiesHit.Count; i++)
            {
                Instantiate(_destroyParticlesPrefab, _enemiesHit[i].transform.position, Quaternion.identity);
            }
        }
    }
    private void GenerateLineNoise(int currentIndex, Vector2 startPos, Vector2 endPos)
    {
        for (int i = 0; i < _noiseRange; i++)
        {
            _electricLine.SetPosition(currentIndex + i,
                          (Vector2)_electricLine.GetPosition(currentIndex + 1 - i) + new Vector2(Random.Range(0, _maxDeviation), Random.Range(0, _maxDeviation)));
        }

    }
}
