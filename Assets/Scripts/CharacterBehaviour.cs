using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : SerializedMonoBehaviour
{
    [PropertySpace]
    [Title("Components")]
    [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }

    public Character Character { get; private set; }

    public Vector2Int Position { get; private set; }

    private List<Ability> _abilities = new List<Ability>();

    public void Initialize(Character character)
    {
        Character = character;
        SpriteRenderer.sprite = character.sprite;
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // Clone each ability and add to this characters list of abilities
        foreach (Ability ability in Character.Abilities)
        {
            Ability newAbility = ability.Clone();
            newAbility.Initialize(character, this);
            _abilities.Add(newAbility);
        }
    }

    public void TakeTurn()
    {
        foreach (Ability ability in _abilities)
        {
            ability.TakeTurn();
        }
    }

    public void Move(Vector2Int movement)
    {
        Position += movement;
        Flip(movement.x);

        Character.MoveTween.Animate(this, Position, TurnManager.TurnInterval);
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
