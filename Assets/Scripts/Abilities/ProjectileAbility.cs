using UnityEngine;

public class ProjectileAbility : Ability
{
    [SerializeField] private ProjectileBehaviour _projectileBehaviourPrefab;

    public override void Execute()
    {
        Enemy nearestEnemy = EnemyManager.GetNearest(_entity.Position);
        if (nearestEnemy == null)
        {
            return;
        }

        var projectileBehaviour = GameObject.Instantiate(_projectileBehaviourPrefab, _entity.transform.position, Quaternion.identity);
        projectileBehaviour.SetTarget(nearestEnemy);
    }
}
