using UnityEngine;

public abstract class Ability
{
    [SerializeField] private int _turns;

    private int _currentTurns;

    protected Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _currentTurns = _turns;
    }

    public void Turn()
    {
        _currentTurns--;

        if(_currentTurns <= 0)
        {
            Execute();
            _currentTurns = _entity.Stats.GetTurnSpeed(_turns);
        }
    }

    public abstract void Execute();
}
