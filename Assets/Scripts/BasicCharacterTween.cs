using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class BasicCharacterTween : CharacterTween
{
    [SerializeField] private Ease _ease = Ease.OutCubic;

    public override void Animate(CharacterBehaviour characterBehaviour, Vector2Int newPosition, float duration)
    {
        characterBehaviour.transform.DOMove((Vector2)newPosition, TurnManager.TurnInterval).SetEase(_ease);
    }
}
