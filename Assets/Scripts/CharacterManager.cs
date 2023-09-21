using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private CharacterBehaviour _characterPrefab;

    [Space]
    [Header("DEBUG")]
    [SerializeField] private List<CharacterBehaviour> _characterBehaviours = new List<CharacterBehaviour>();

    [SerializeField] private List<Character> _testCharacters = new List<Character>();

    private void OnEnable()
    {
        TurnManager.TurnUpdate += OnTurnUpdate;
    }

    private void OnDisable()
    {
        TurnManager.TurnUpdate -= OnTurnUpdate;
    }

    private void Start()
    {
        foreach (Character character in _testCharacters)
        {
            AddCharacter(character);
        }
    }

    private void OnTurnUpdate()
    {
        bool first = true;
        Vector2Int previousCharacterPosition = Vector2Int.zero;
        foreach (CharacterBehaviour characterBehaviour in _characterBehaviours)
        {
            characterBehaviour.TakeTurn();

            if (first)
            {
                previousCharacterPosition = characterBehaviour.Position;
                characterBehaviour.Move(InputManager.Axis);
                first = false;
                continue;
            }

            // Move character towards previous character
            Vector2Int temp = characterBehaviour.Position;
            characterBehaviour.Move(new Vector2Int(previousCharacterPosition.x - characterBehaviour.Position.x, previousCharacterPosition.y - characterBehaviour.Position.y));
            previousCharacterPosition = temp;
        }
    }

    public void AddCharacter(Character character)
    {
        // find new character position
        CharacterBehaviour lastCharacterBehaviour = _characterBehaviours.LastOrDefault();
        Vector2 newCharacterPosition = lastCharacterBehaviour == null ? transform.position : new Vector2(lastCharacterBehaviour.Position.x, lastCharacterBehaviour.Position.y - 1);

        // instantiate character
        CharacterBehaviour newCharacterBehaviour = Instantiate(_characterPrefab, newCharacterPosition, Quaternion.identity);
        newCharacterBehaviour.transform.SetParent(gameObject.transform);

        // initialize character with scriptable object
        newCharacterBehaviour.Initialize(character);

        // add character to line list
        _characterBehaviours.Add(newCharacterBehaviour);
    }
}
