using UnityEngine;

[System.Serializable]
public abstract class EntityTween
{
    public abstract void Animate(Entity entity, Vector2Int newPosition, float duration);
}
