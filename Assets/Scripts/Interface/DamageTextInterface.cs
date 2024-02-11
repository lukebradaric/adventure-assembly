using AdventureAssembly.Units;
using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Enemies;
using DG.Tweening;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Interface
{
    /// <summary>
    /// Interface for handling damage number text.
    /// </summary>
    public class DamageTextInterface : MonoBehaviour
    {
        [Space]
        [Header("Prefabs")]
        [SerializeField] private DamageTextElement _damageTextPrefab;

        [Space]
        [Header("Colors")]
        [SerializeField] private Color _damageColor;
        [SerializeField] private Color _damageBackgroundColor;
        [SerializeField] private Color _criticalDamageColor;
        [SerializeField] private Color _healColor;
        [SerializeField] private Color _healBackgroundColor;

        [Space]
        [Header("Settings")]
        [Tooltip("How far away should the damage text move from its origin?")]
        [SerializeField] private float _tweenDistance;
        [Tooltip("How long should the tween animation be?")]
        [SerializeField] private float _tweenDuration;
        [SerializeField] private Ease _movementEase;
        [SerializeField] private Ease _fadeEase;

        public void OnEnemyDamaged(GameEventData gameEventData)
        {
            OnEnemyDamaged((Enemy)gameEventData.Sender, (DamageData)gameEventData.Data);
        }

        public void OnCharacterHealed(GameEventData gameEventData)
        {
            OnCharacterHealed((Character)gameEventData.Sender, (HealData)gameEventData.Data);
        }

        private void OnEnemyDamaged(Enemy enemy, DamageData damageData)
        {
            CreateText(
                enemy.transform.position,
                damageData.IsCritical ? damageData.Value.ToString() + "!" : damageData.Value.ToString(),
                damageData.IsCritical ? _criticalDamageColor : _damageColor,
                _damageBackgroundColor,
                damageData.Direction == Vector2.zero ? GetRandomDirection() : damageData.Direction
                );
        }

        private void OnCharacterHealed(Character character, HealData healData)
        {
            CreateText(
                character.transform.position,
                healData.Value.ToString(),
                _healColor,
                _healBackgroundColor,
                GetRandomDirection()
                );
        }

        private Vector2 GetRandomDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        private void CreateText(Vector2 position, string text, Color color, Color backgroundColor, Vector2 movement)
        {
            // TODO: We should probably pool these because its easy

            // Get spawn position in screen coords
            Vector2 spawnPosition = Camera.main.WorldToScreenPoint(position);

            // Spawn text and set properties
            DamageTextElement damageText = Instantiate(_damageTextPrefab, spawnPosition, Quaternion.identity);
            damageText.transform.SetParent(this.transform);
            damageText.Text = text;
            damageText.BackgroundColor = backgroundColor;
            damageText.Color = color;

            // Tween text
            damageText.transform.DOMove(movement * _tweenDistance, _tweenDuration).SetEase(_movementEase).SetRelative(true);
            damageText.DOFade(0f, _tweenDuration, _tweenDuration * 1.1f).SetEase(_fadeEase).OnComplete(() =>
            {
                Destroy(damageText.gameObject);
            });
        }
    }
}