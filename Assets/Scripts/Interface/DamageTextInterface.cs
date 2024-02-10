﻿using AdventureAssembly.Units.Enemies;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    /// <summary>
    /// Interface for handling damage number text.
    /// </summary>
    public class DamageTextInterface : MonoBehaviour
    {
        [Space]
        [Header("Events")]
        [SerializeField] private EnemyScriptableEvent _enemyDamagedScriptableEvent;

        [Space]
        [Header("Prefabs")]
        [SerializeField] private DamageTextElement _damageTextPrefab;

        [Space]
        [Header("Settings")]
        [SerializeField] private Color _criticalColor;
        [Tooltip("How far away should the damage text move from its origin?")]
        [SerializeField] private float _tweenDistance;
        [Tooltip("How long should the tween animation be?")]
        [SerializeField] private float _tweenDuration;
        [SerializeField] private Ease _movementEase;
        [SerializeField] private Ease _fadeEase;

        private void OnEnable()
        {
            _enemyDamagedScriptableEvent.Event += OnEnemyDamaged;
        }

        private void OnDisable()
        {
            _enemyDamagedScriptableEvent.Event += OnEnemyDamaged;
        }

        private void OnEnemyDamaged(Enemy enemy)
        {
            // TODO: We should probably pool these because its easy

            // Get spawn position of damage text
            Vector2 spawnPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);

            // Spawn damage text
            DamageTextElement damageText = Instantiate(_damageTextPrefab, spawnPosition, Quaternion.identity);
            damageText.transform.SetParent(this.transform);
            damageText.Text = enemy.LastDamageTaken.Value.ToString();
            if (enemy.LastDamageTaken.IsCritical)
            {
                damageText.Color = _criticalColor;
            }

            // Get random direction to move damage text
            Vector2 movement = enemy.LastDamageTaken.Direction;
            while (movement == Vector2.zero)
            {
                movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }

            // Tween damage text
            damageText.transform.DOMove(movement.normalized * _tweenDistance, _tweenDuration).SetEase(_movementEase).SetRelative(true);
            damageText.DOFade(0f, _tweenDuration).SetEase(_fadeEase).OnComplete(() =>
            {
                Destroy(damageText.gameObject);
            });
        }
    }
}