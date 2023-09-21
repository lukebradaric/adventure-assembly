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
        Debug.Log($"Moving {InputManager.Axis}");

        bool first = true;
        CharacterBehaviour previousCharacter = null;
        foreach (CharacterBehaviour characterBehaviour in _characterBehaviours)
        {
            if (first)
            {
                characterBehaviour.Move(InputManager.Axis);
                first = false;
                previousCharacter = characterBehaviour;
                continue;
            }

            // Move character towards previous character
            characterBehaviour.Move(new Vector2Int(previousCharacter.Position.x - characterBehaviour.Position.x, previousCharacter.Position.y - characterBehaviour.Position.y));
            previousCharacter = characterBehaviour;
        }
    }

    public void AddCharacter(Character character)
    {
        // Spawn character at end of line
        Vector2 newCharacterPosition = _characterBehaviours.LastOrDefault() == null ? transform.position : new Vector2(_characterBehaviours.Last().Position.x, _characterBehaviours.Last().Position.y - 1);
        CharacterBehaviour newCharacterBehaviour = Instantiate(_characterPrefab, newCharacterPosition, Quaternion.identity);

        // initialize character with scriptable object
        newCharacterBehaviour.Initialize(character);

        // add character to line list
        _characterBehaviours.Add(newCharacterBehaviour);
    }
}
