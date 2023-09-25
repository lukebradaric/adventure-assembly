using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ChargeAbility : Ability
{
    [Space]
    [Header("Stats")]
    [SerializeField] private float _chargeTime;
    [SerializeField] private int _damage;
    [Space]
    [Header("Components")]
    [SerializeField] private GameObject _spriteRenderTransform;
    private Enemy _target;
    public override void Execute()
    {
        //Get Target
        _target = EnemyManager.GetNearest(_entity.transform.position);
        ContactDamage contactDamage = _spriteRenderTransform.GetComponent<ContactDamage>();
        
        //Enable Collider in time with charge
        contactDamage.StartCoroutine(contactDamage.EnableCollider(0));
        contactDamage.StartCoroutine(contactDamage.DisableCollider(_chargeTime));
        //DOTween sequence for moving character
        Sequence doSequence = DOTween.Sequence();
        doSequence.Append(_spriteRenderTransform.transform.DOMove(_target.transform.position, _chargeTime));
        doSequence.Append(_spriteRenderTransform.transform.DOLocalMove(Vector3.zero, _chargeTime));

    }
}
