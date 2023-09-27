using DG.Tweening;
using System;
using TinyTools.AutoLoad;
using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private GameObject _damageTextPrefab = default;
    [SerializeField] private Color _characterDamagedColor = Color.red;
    [SerializeField] private Color _enemyDamagedColor = Color.white;
    [SerializeField] private float _tweenDuration = 1f;

    private void OnEnable()
    {
        Entity.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        Entity.Damaged -= OnDamaged;
    }

    private void OnDamaged(Entity entity, int damage)
    {
        GameObject damageTextGameObject = Instantiate(_damageTextPrefab, entity.transform.position, Quaternion.identity);
        TextMeshProUGUI damageText = damageTextGameObject.GetComponentInChildren<TextMeshProUGUI>();
        damageText.text = damage.ToString();

        if (entity is Character)
        {
            damageText.color = _characterDamagedColor;
        }
        else if (entity is Enemy)
        {
            damageText.color = _enemyDamagedColor;
        }

        Vector2 newPos = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) + (Vector2)damageTextGameObject.transform.position;
        damageTextGameObject.transform.DOLocalMove(newPos, _tweenDuration).SetEase(Ease.OutQuint);
        damageText.DOColor(Color.clear, _tweenDuration).SetEase(Ease.InQuint);
    }
}
