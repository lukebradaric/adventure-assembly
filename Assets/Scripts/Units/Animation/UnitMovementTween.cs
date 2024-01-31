using AdventureAssembly.Units;
using AdventureAssembly.Units.Characters;
using UnityEngine;

namespace AdventureAssembly.Units.Animation
{
    [System.Serializable]
    public abstract class UnitMovementTween
    {
        public abstract void Animate(CharacterUnit unit, Vector2Int newPosition, float duration);
    }
}