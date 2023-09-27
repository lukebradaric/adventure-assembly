using UnityEngine;

public class StartCharacter : MonoBehaviour
{
    private void Awake()
    {
        CharacterManager.LevelUp(1);
    }
}
