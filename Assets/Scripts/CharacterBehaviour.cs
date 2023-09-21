using DG.Tweening;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Character Character { get; private set; }

    public Vector2Int Position { get; private set; }

    private void Awake()
    {
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    public void Initialize(Character character)
    {
        Character = character;
        _spriteRenderer.sprite = character.sprite;

        foreach (Ability ability in Character.abilties)
        {
            ability.Initialize(character, this);
        }
    }

    public void Move(Vector2Int movement)
    {
        Position += movement;

        // Tween movement
        transform.DOLocalMove((Vector2)movement, TurnManager.TurnInterval);
    }
}
