using System.Collections.Generic;
using UnityEngine;

public class TestAddCharacters : MonoBehaviour
{
    [SerializeField] private List<Character> _testCharacterPrefabs = new List<Character>();

    private void Start()
    {
        foreach (var character in _testCharacterPrefabs)
        {
            CharacterManager.AddCharacter(character);
        }
    }
}
