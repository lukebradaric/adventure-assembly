using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    [SerializeField] private Entity _target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var projectile = Instantiate(_projectile, this.gameObject.transform);
            projectile.GetComponent<ElectricChain>().SetTarget(EnemyManager.GetNearest(this.gameObject.transform.position));
        }
    }
}
