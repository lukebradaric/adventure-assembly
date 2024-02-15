using AdventureAssembly.Units.Characters;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    public class DefaultUnitMovementTween : UnitTween
    {
        public override Tween Animate(Character unit, Vector2Int newPosition, float duration)
        {
            return unit.transform.DOMove((Vector2)newPosition, duration).SetEase(Ease.OutCubic);
        }
    }
}