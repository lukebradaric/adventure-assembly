using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject _knives;

    [SerializeField] private Entity _target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var knives = Instantiate(_knives, this.gameObject.transform);
            knives.GetComponent<Projectile>().SetTarget(_target);
        }
    }
}
