using UnityEngine;

public class ThrowingKnifeAbility : Ability
{
    [SerializeField] private ProjectileBehaviour _projectileBehaviourPrefab;

    public override void Execute()
    {
        Debug.Log("Knife ability execute...");

        var projectileBehaviour = GameObject.Instantiate(_projectileBehaviourPrefab, _entity.transform.position, Quaternion.identity);
        projectileBehaviour.SetTarget(GameObject.Find("Enemy"));
    }
}
