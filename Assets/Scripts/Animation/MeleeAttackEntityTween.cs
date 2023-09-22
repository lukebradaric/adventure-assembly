using DG.Tweening;
using UnityEngine;

public class MeleeAttackEntityTween : EntityTween
{
    //[SerializeField] private Ease _startEase;
    //[SerializeField] private Ease _ease;

    public override void Animate(Entity entity, Vector2Int newPosition, float duration)
    {
        Sequence sequence = DOTween.Sequence();

        Vector2 directionToAttack = ((Vector2)(newPosition - entity.Position)).normalized;
        sequence.Append(entity.SpriteRenderer.transform.DOLocalMove((-directionToAttack * 0.35f), duration * 0.15f)).SetRelative();
        sequence.Append(entity.SpriteRenderer.transform.DOLocalMove(directionToAttack, duration * 0.15f)).SetRelative();
        sequence.Append(entity.SpriteRenderer.transform.DOLocalMove(Vector2.zero, duration * 0.7f));
    }
}
