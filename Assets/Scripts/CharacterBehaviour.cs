using DG.Tweening;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Character Character { get; private set; }

    public Vector2Int Position { get; private set; }

    public void Initialize(Character character)
    {
        Character = character;
        _spriteRenderer.sprite = character.sprite;
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        foreach (Ability ability in Character.abilties)
        {
            ability.Initialize(character, this);
        }
    }

    public void Move(Vector2Int movement)
    {
        Position += movement;
        Flip(movement.x);
        transform.DOMove((Vector2)Position, TurnManager.TurnInterval).SetEase(Ease.OutCubic);
    }

    private void Flip(int x)
    {
        if (x == 0)
        {
            return;
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(x) * 1;
        transform.localScale = scale;
    }
}
