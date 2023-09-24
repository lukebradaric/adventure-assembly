using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAbility : Ability
{
    [SerializeField] private Projectile _dropProjectile;
    public override void Execute()
    {
        GameObject.Instantiate(_dropProjectile, _entity.transform.position, Quaternion.identity);
    }
}
