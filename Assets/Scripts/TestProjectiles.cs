using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject _knives;

    [SerializeField] private GameObject _target;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var knives = Instantiate(_knives, this.gameObject.transform);
            knives.GetComponent<ProjectileBehaviour>().SetTarget(_target);
        }
    }
}
