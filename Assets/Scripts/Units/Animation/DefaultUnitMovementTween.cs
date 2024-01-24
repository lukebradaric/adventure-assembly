using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    public class DefaultUnitMovementTween : UnitMovementTween
    {
        public override void Animate(Unit unit, Vector2Int newPosition, float duration)
        {
            unit.transform.DOMove((Vector2)newPosition, duration).SetEase(Ease.OutCubic);
        }
    }
}