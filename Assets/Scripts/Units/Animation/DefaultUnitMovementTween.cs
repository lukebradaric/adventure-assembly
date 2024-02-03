using AdventureAssembly.Units.Characters;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    public class DefaultUnitMovementTween : UnitTween
    {
        public override void Animate(CharacterUnit unit, Vector2Int newPosition, float duration)
        {
            unit.transform.DOMove((Vector2)newPosition, duration).SetEase(Ease.OutCubic);
        }
    }
}