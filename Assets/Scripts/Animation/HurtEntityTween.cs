using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class HurtEntityTween : EntityTween
{
    public override void Animate(Entity entity, Vector2Int newPosition, float duration)
    {
        entity.SpriteRenderer.transform.localScale = Vector3.one;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(entity.SpriteRenderer.transform.DOScaleY(0.75f, duration / 2));
        sequence.Append(entity.SpriteRenderer.transform.DOScaleY(1, duration / 2));
        //entity.SpriteRenderer.transform.DOScaleY(0.75f, duration);
        //entity.SpriteRenderer.transform.DOPunchScale(-Vector3.one * 0.2f, duration);

        float value = 0;
        DOTween.To(() => value, x => value = x, 0.8f, duration / 2).OnUpdate(() =>
        {
            Debug.Log("tweenin");
            entity.SpriteRenderer.material.SetColor("_Color", new Color(value, value, value));
        }).OnComplete(() =>
        {
            DOTween.To(() => value, x => value = x, 0, duration / 2).OnUpdate(() =>
            {
                entity.SpriteRenderer.material.SetColor("_Color", new Color(value, value, value));
            });
        });
    }
}
