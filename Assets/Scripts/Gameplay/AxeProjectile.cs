using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AxeProjectile : Projectile
{
    [SerializeField] private int _spinTurns;

    private Transform originalOwner;
    protected override void Awake()
    {
        Destroy(gameObject, _spinTurns * TurnManager.TurnInterval);
    }
    protected override void FixedUpdate()
    {
        this.transform.position = originalOwner.transform.position;
    }
    protected override void OnEnemyCollision(Enemy enemy)
    {
        Debug.Log("Hitting enemies");
        enemy.Damage(_damage);
    }
    private void OnEnable()
    {
        transform.DOLocalRotate(new Vector3(0, 0, -360), TurnManager.TurnInterval * _spinTurns, RotateMode.FastBeyond360);
        originalOwner = CharacterManager.GetNearest(this.transform.position).transform;
    }
}
