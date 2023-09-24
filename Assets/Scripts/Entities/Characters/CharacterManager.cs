using System;
using System.Collections.Generic;
using System.Linq;
using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class CharacterManager : EntityManagerBase<Character>
{
    public static event Action LeveledUp;

    private static Vector2Int _lastInputAxis;

    private static int _currentLevel = 0;
    private static float _currentExperience;

    private static CharacterListScriptableVariable _characterList;

    private void Awake()
    {
        _characterList = Resources.Load("AllCharactersListScriptableVariable") as CharacterListScriptableVariable;
    }

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
        // Is this the first character in the loop
        bool first = true;

        // Position of the previous character
        Vector2Int previousCharacterPosition = Vector2Int.zero;

        // Movement axis (If new input is just negative of previous, ignore it) (YOU CAN MOVE BACKWARDS INTO YOURSELF, DUHHH)
        Vector2Int moveAxis = InputManager.Axis == -_lastInputAxis ? _lastInputAxis : InputManager.Axis;

        // Characters to kill due to collision
        List<Character> killCharacters = new List<Character>();
        // The first character to die in a collision kill
        Character _startKillCharacter = null;

        foreach (Character character in Entities)
        {
            // If we reached the kill character, exit the loop
            if (character == _startKillCharacter)
            {
                break;
            }

            character.Turn();

            // If this is the first character in the snake
            if (first)
            {
                previousCharacterPosition = character.Position;

                // If you run into yourself, KILL YOUSELF
                if (TryGet(character.Position + moveAxis, out Character c2))
                {
                    // Kill all characters after the one we ran into
                    _startKillCharacter = c2;
                    int index = Entities.IndexOf(c2);
                    killCharacters.AddRange(Entities.GetRange(index, Entities.Count - index));
                }

                character.Move(moveAxis);
                first = false;
                continue;
            }

            // For all subsequence characters after the first
            // Move character towards previous character
            Vector2Int temp = character.Position;
            character.Move(new Vector2Int(previousCharacterPosition.x - character.Position.x, previousCharacterPosition.y - character.Position.y));
            previousCharacterPosition = temp;
        }

        // Kill all characters in the kill list (collision death)
        foreach (Character character in killCharacters)
        {
            character.Die();
        }

        // Update the last valid input axis
        _lastInputAxis = moveAxis;
    }

    public static void AddCharacter(Character characterPrefab)
    {
        // find new character position
        Character lastCharacterBehaviour = Entities.LastOrDefault();
        Vector2 newCharacterPosition = lastCharacterBehaviour == null ? Vector2.zero : new Vector2(lastCharacterBehaviour.Position.x, lastCharacterBehaviour.Position.y - 1);

        // instantiate character
        Instantiate(characterPrefab, newCharacterPosition, Quaternion.identity);
    }

    public static void AddExperience(float experience)
    {
        _currentExperience += experience;

        int newLevel = (int)Mathf.Sqrt(_currentExperience);
        if (newLevel - _currentLevel > 0)
        {
            _currentLevel = newLevel;
            LeveledUp?.Invoke();
        }
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

    public static Bounds GetBounds()
    {
        Vector2Int maxPos = Vector2Int.zero;
        Vector2Int minPos = Vector2Int.zero;

        Vector3[] positions = new Vector3[Entities.Count];
        for (int i = 0; i < Entities.Count; i++)
        {
            positions[i] = (Vector2)Entities[i].Position;
        }

        return GeometryUtility.CalculateBounds(positions, Matrix4x4.identity);
    }
}
