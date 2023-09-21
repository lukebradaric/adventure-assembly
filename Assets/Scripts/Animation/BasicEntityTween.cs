using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class BasicEntityTween : EntityTween
{
    [SerializeField] private Ease _ease = Ease.OutCubic;

    public override void Animate(Entity entity, Vector2Int newPosition, float duration)
    {
        entity.transform.DOMove((Vector2)newPosition, TurnManager.TurnInterval).SetEase(_ease);
    }
}
