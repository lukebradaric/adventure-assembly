using TinyTools.ScriptableSounds;
using UnityEngine;

public class ProjectileAbility : Ability
{
    [Space]
    [Header("Components")]
    [SerializeField] private Projectile _projectileBehaviourPrefab;
    [SerializeField] private ScriptableSound _projectileSound;

    public override void Execute()
    {
        Enemy nearestEnemy = EnemyManager.GetNearest(_entity.Position);
        if (nearestEnemy == null)
        {
            return;
        }

        _projectileSound?.Play();
        var projectileBehaviour = GameObject.Instantiate(_projectileBehaviourPrefab, _entity.transform.position, Quaternion.identity);
        projectileBehaviour.SetTarget(nearestEnemy);
    }
}
