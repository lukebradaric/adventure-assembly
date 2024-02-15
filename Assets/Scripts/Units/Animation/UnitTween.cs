using AdventureAssembly.Units.Characters;
using DG.Tweening;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    [System.Serializable]
    public abstract class UnitTween
    {
        public abstract Tween Animate(Character unit, Vector2Int newPosition, float duration);
    }
}