using System.Collections.Generic;
using System.Linq;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class CharacterManager : EntityManager<Character>
{
    private static List<Character> _characters = new List<Character>();

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
        foreach (Character character in _characters)
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
        Character lastCharacterBehaviour = _characters.LastOrDefault();
        Vector2 newCharacterPosition = lastCharacterBehaviour == null ? Vector2.zero : new Vector2(lastCharacterBehaviour.Position.x, lastCharacterBehaviour.Position.y - 1);

        // instantiate character
        Character newCharacter = Instantiate(characterPrefab, newCharacterPosition, Quaternion.identity);
        //newCharacter.transform.SetParent(gameObject.transform);

        // initialize character with scriptable object
        newCharacter.Initialize();

        // add character to line list
        _characters.Add(newCharacter);
    }
}
