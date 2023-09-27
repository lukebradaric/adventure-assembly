using System;
using System.Collections.Generic;
using System.Linq;
using TinyTools.AutoLoad;
using UnityEngine;

public class CharacterManager : EntityManagerBase<Character>
{
    [SerializeField] private GameObject _firstCharacterIndicatorPrefab = default;

    public static event Action LeveledUp;

    private static Vector2Int _lastInputAxis;

    private static int _currentLevel = 0;
    private static float _currentExperience;

    private static Dictionary<Class, List<ClassModifierInstance>> _classModifiers = new Dictionary<Class, List<ClassModifierInstance>>();
    private static List<ClassModifierInstance> _globalModifiers = new List<ClassModifierInstance>();

    private static Transform _characterParentTransform = null;
    private static GameObject _firstCharacterIndicator;

    private void Awake()
    {
        _characterParentTransform = new GameObject("CharacterParentTransform").transform;
    }

    private void OnEnable()
    {
        TurnManager.TurnUpdate += OnTurnUpdate;
        Unregistered += OnUnregistered;
    }

    private void OnDisable()
    {
        TurnManager.TurnUpdate -= OnTurnUpdate;
        Unregistered -= OnUnregistered;
    }

    private void Start()
    {
        _firstCharacterIndicator = GameObject.Instantiate(_firstCharacterIndicatorPrefab);
    }

    private void OnUnregistered()
    {
        if(Entities.Count == 0)
        {
            Debug.Log("game lost");
        }
    }

    private static void OnTurnUpdate()
    {
        _firstCharacterIndicator.transform.SetParent(Entities.First().transform, false);

        // Is this the first character in the loop
        bool first = true;

        // Position of the previous character
        Vector2Int previousCharacterPosition = Vector2Int.zero;

        // Movement axis (If new input is just negative of previous, ignore it) (YOU CANT MOVE BACKWARDS INTO YOURSELF, DUHHH)
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

                // If you run into a wall, Kill YA SELF
                Collider2D hitCollider = Physics2D.OverlapPoint(character.Position + moveAxis);
                if (hitCollider != null && hitCollider.CompareTag("Border"))
                {
                    killCharacters.Add(character);
                    first = false;

                    int charIndex = Entities.IndexOf(character);
                    if (Entities.Count > charIndex + 1)
                    {
                        _firstCharacterIndicator.transform.SetParent(Entities[charIndex + 1].transform, false);
                    }

                    continue;
                }

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
        Character character = Instantiate(characterPrefab, newCharacterPosition, Quaternion.identity);
        character.transform.SetParent(_characterParentTransform);

        // Add global modifiers to new character
        foreach (ClassModifierInstance modifier in _globalModifiers)
        {
            character.Stats.ChangeValue(modifier.statName, modifier.value);
        }

        // Add class modifiers to new character
        foreach (Class cl in character.Classes)
        {
            if (!_classModifiers.ContainsKey(cl))
            {
                continue;
            }

            foreach (ClassModifierInstance modifier in _classModifiers[cl])
            {
                character.Stats.ChangeValue(modifier.statName, modifier.value);
            }
        }
    }

    public static void AddExperience(float experience)
    {
        _currentExperience += experience;

        int newLevel = (int)Mathf.Sqrt(_currentExperience);
        if (newLevel - _currentLevel > 0)
        {
            LevelUp(newLevel);
        }
    }

    public static void LevelUp(int newLevel)
    {
        _currentLevel = newLevel;
        LeveledUp?.Invoke();
    }

    public static void AddGlobalModifier(ClassModifierInstance modifier)
    {
        _globalModifiers.Add(modifier);
        Debug.Log($"Global Modifier Added {modifier.statName}: {modifier.value}");

        foreach (Character character in Entities)
        {
            character.Stats.ChangeValue(modifier.statName, modifier.value);
        }
    }

    public static void RemoveGlobalModifier(ClassModifierInstance modifier)
    {
        _globalModifiers.Remove(modifier);
        Debug.Log($"Global Modifier Removed {modifier.statName}: {modifier.value}");

        foreach (Character character in Entities)
        {
            character.Stats.ChangeValue(modifier.statName, -modifier.value);
        }
    }

    public static void AddClassModifier(Class cl, ClassModifierInstance modifier)
    {
        Debug.Log($"Class Modifier Added {cl.name} - {modifier.statName}: {modifier.value}");

        if (!_classModifiers.ContainsKey(cl))
        {
            _classModifiers.Add(cl, new List<ClassModifierInstance> { modifier });
        }

        foreach (Character character in Entities)
        {
            if (!character.Classes.Contains(cl))
            {
                continue;
            }

            character.Stats.ChangeValue(modifier.statName, modifier.value);
        }
    }

    public static void RemoveClassModifier(Class cl, ClassModifierInstance modifier)
    {
        _classModifiers[cl].Remove(modifier);
        Debug.Log($"Class Modifier Removed {cl.name} - {modifier.statName}: {modifier.value}");

        foreach (Character character in Entities)
        {
            if (!character.Classes.Contains(cl))
            {
                continue;
            }

            character.Stats.ChangeValue(modifier.statName, -modifier.value);
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
