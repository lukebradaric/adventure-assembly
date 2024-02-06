using AdventureAssembly.Units.Characters;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    public class DefaultEnemyAttackTween : UnitTween
    {
        public override void Animate(Character unit, Vector2Int newPosition, float duration)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(unit.transform.DOMove((Vector2)newPosition, duration * 0.3f));
            sequence.Append(unit.transform.DOMove((Vector2)unit.Position, duration * 0.7f));
        }
    }
}