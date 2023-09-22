using DG.Tweening;
using UnityEngine;

public class MeleeAttackEntityTween : EntityTween
{
    public override void Animate(Entity entity, Vector2Int newPosition, float duration)
    {
        Sequence sequence = DOTween.Sequence();

        //Vector2 directionToAttack = ((Vector2)newPosition - (Vector2)entity.Position).normalized;
        //sequence.Append(entity.SpriteRenderer.transform.DOLocalMove((-(Vector2)newPosition * 0.25f), duration * 0.2f));
        sequence.Append(entity.SpriteRenderer.transform.DOMove((Vector2)newPosition, duration * 0.2f));
        sequence.Append(entity.SpriteRenderer.transform.DOLocalMove(Vector2.zero, duration * 0.6f));
    }
}
