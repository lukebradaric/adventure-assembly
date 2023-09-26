using UnityEngine;
using DG.Tweening;


public class ChargeAbility : Ability
{
    [Space]
    [Header("Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private int _stunTurns;
    [SerializeField] private float _maxRange;

    [Space]
    [Header("Components")]
    [SerializeField] private GameObject _spriteRenderTransform;

    private Enemy _target;

    public override void Execute()
    {
        //Get Target
        _target = EnemyManager.GetNearest(_entity.transform.position);
        if (_target == null || Vector2.Distance(_entity.transform.position, _target.transform.position) > _maxRange)
        {
            return;
        }

        _target.AddStun(_stunTurns);

        Sequence doSequence = DOTween.Sequence();
        doSequence.Append(_spriteRenderTransform.transform.DOMove(_target.transform.position, TurnManager.TurnInterval / 2).OnComplete(() =>
        {
            _target.Damage(_entity.Stats.GetDamage(_damage));
        }));
        doSequence.Append(_spriteRenderTransform.transform.DOLocalMove(Vector3.zero, TurnManager.TurnInterval / 2));
    }
}
