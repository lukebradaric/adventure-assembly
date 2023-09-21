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
        _currentTurns = _turns;
    }

    public void TakeTurn()
    {
        _currentTurns--;

        if(_currentTurns <= 0)
        {
            Execute();
            _currentTurns = _turns;
        }
    }

    public Ability Clone()
    {
        return (Ability)this.MemberwiseClone();
    }

    public abstract void Execute();
}
