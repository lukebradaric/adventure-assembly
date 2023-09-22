using UnityEngine;

public class ProjectileAbility : Ability
{
    [SerializeField] private ProjectileBehaviour _projectileBehaviourPrefab;

    public override void Execute()
    {
        var projectileBehaviour = GameObject.Instantiate(_projectileBehaviourPrefab, _entity.transform.position, Quaternion.identity);
        projectileBehaviour.SetTarget(GameObject.Find("Enemy"));
    }
}
