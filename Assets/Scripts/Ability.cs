using UnityEngine;

public abstract class Ability
{
    [SerializeField] private int _turns;

    private int _currentTurns;

    protected Character _character;
    protected CharacterBehaviour _characterBehaviour;

    public void Initialize(Character character, CharacterBehaviour characterBehaviour)
    {
        _character = character;
        _characterBehaviour = characterBehaviour;
    }

    public abstract void Execute();
}
