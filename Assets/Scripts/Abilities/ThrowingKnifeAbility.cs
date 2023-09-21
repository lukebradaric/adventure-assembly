using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeAbility : Ability
{
    private GameObject _targetPosition;

    [SerializeField] private GameObject _knife;
    public override void Execute()
    {
        //TODO: Get position of nearby enemy, and throw knives at it
        //_targetPosition = Manager.GetNearestEnemy();
        Debug.Log("Knife ability execute...");

        var knife = GameObject.Instantiate(_knife, _characterBehaviour.transform.position, Quaternion.identity);

    }
}
