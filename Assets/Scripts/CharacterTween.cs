using UnityEngine;

[System.Serializable]
public abstract class CharacterTween
{
    public abstract void Animate(CharacterBehaviour characterBehaviour, Vector2Int newPosition, float duration);
}
