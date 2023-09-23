using UnityEngine;

public class AreaDamageParticleSystem : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private AreaDamage _areaDamage;
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnValidate()
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        if (_areaDamage == null)
        {
            _areaDamage = GetComponent<AreaDamage>();
        }

        _particleSystem.startLifetime = _areaDamage.TurnDuration * TurnManager.TurnInterval;
    }
}
