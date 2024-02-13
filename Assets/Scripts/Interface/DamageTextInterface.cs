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
        [SerializeField] private DamageTextElement _criticalDamageTextPrefab;
        [SerializeField] private DamageTextElement _healTextPrefab;

        [Space]
        [Header("Settings")]
        [Tooltip("How far away should the damage text move from its origin?")]
        [SerializeField] private float _tweenDistance;
        [Tooltip("How long should the tween animation be?")]
        [SerializeField] private float _tweenDuration;
        [SerializeField] private Ease _movementEase;

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
                damageData.IsCritical ? _criticalDamageTextPrefab : _damageTextPrefab,
                damageData.IsCritical ? damageData.Value.ToString() + "!" : damageData.Value.ToString(),
                enemy.transform.position,
                damageData.Direction == Vector2.zero ? GetRandomDirection() : damageData.Direction
                );
        }

        private void OnCharacterHealed(Character character, HealData healData)
        {
            CreateText(
                _healTextPrefab,
                healData.Value.ToString(),
                character.transform.position,
                GetRandomDirection()
                );
        }

        private Vector2 GetRandomDirection()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        private void CreateText(DamageTextElement prefab, string text, Vector2 position, Vector2 movement)
        {
            // TODO: We should probably pool these because its easy

            // Get spawn position in screen coords
            Vector2 spawnPosition = Camera.main.WorldToScreenPoint(position);

            // Spawn text and set properties
            DamageTextElement damageText = Instantiate(prefab, spawnPosition, Quaternion.identity);
            damageText.transform.SetParent(this.transform);
            damageText.Text = text;

            // Tween text
            damageText.transform.DOMove(movement * _tweenDistance, _tweenDuration).SetEase(_movementEase).SetRelative(true);

            damageText.DoFadeOutTween().OnComplete(() =>
            {
                Destroy(damageText.gameObject);
            });
        }
    }
}