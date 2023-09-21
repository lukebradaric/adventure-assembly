using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class WindUpEntityTween : EntityTween
{
    [SerializeField] private Ease _startEase;
    [SerializeField] private Ease _ease;

    public override void Animate(Entity entity, Vector2Int newPosition, float duration)
    {
        Sequence rotSequence = DOTween.Sequence();

        rotSequence.Append(entity.SpriteRenderer.transform.DORotate(new Vector3(0, 0, 15 * entity.transform.localScale.x), duration * 0.2f).SetEase(_startEase));
        rotSequence.Append(entity.SpriteRenderer.transform.DORotate(new Vector3(0, 0, 0), duration * 0.8f).SetEase(_ease));

        Sequence sequence = DOTween.Sequence();

        Vector3 newPos = (Vector2)newPosition;
        sequence.Append(entity.transform.DOMove(entity.transform.position - (newPos.normalized * 0.15f), duration * 0.2f).SetEase(_startEase));
        sequence.Append(entity.transform.DOMove((Vector2)newPosition, duration * 0.8f).SetEase(_ease));
    }
}
