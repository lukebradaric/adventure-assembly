using System.Linq;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class CharacterManager : EntityManagerBase<Character>
{
    private void OnEnable()
    {
        TurnManager.TurnUpdate += OnTurnUpdate;
    }

    private void OnDisable()
    {
        TurnManager.TurnUpdate -= OnTurnUpdate;
    }

    private static void OnTurnUpdate()
    {
        bool first = true;
        Vector2Int previousCharacterPosition = Vector2Int.zero;
        foreach (Character character in Entities)
        {
            character.Turn();

            if (first)
            {
                previousCharacterPosition = character.Position;
                character.Move(InputManager.Axis);
                first = false;
                continue;
            }

            // Move character towards previous character
            Vector2Int temp = character.Position;
            character.Move(new Vector2Int(previousCharacterPosition.x - character.Position.x, previousCharacterPosition.y - character.Position.y));
            previousCharacterPosition = temp;
        }
    }

    public static void AddCharacter(Character characterPrefab)
    {
        // find new character position
        Character lastCharacterBehaviour = Entities.LastOrDefault();
        Vector2 newCharacterPosition = lastCharacterBehaviour == null ? Vector2.zero : new Vector2(lastCharacterBehaviour.Position.x, lastCharacterBehaviour.Position.y - 1);

        // instantiate character
        Instantiate(characterPrefab, newCharacterPosition, Quaternion.identity);
    }

    public static Vector2 GetCenter()
    {
        Vector2 total = Vector2.zero;

        foreach (Character character in Entities)
        {
            total += character.Position;
        }

        return total / Entities.Count;
    }
}
