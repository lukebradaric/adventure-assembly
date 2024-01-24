using AdventureAssembly.Units;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    [System.Serializable]
    public abstract class UnitMovementTween
    {
        public abstract void Animate(Unit unit, Vector2Int newPosition, float duration);
    }
}