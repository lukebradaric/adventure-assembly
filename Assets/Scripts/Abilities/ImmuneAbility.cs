using System.Collections.Generic;
using TinyTools.ScriptableSounds;
using UnityEngine;

public class ImmuneAbility : Ability
{
    [SerializeField] private int _numOfMaxShields;
    [SerializeField] private float _shieldingRadius;
    [SerializeField] private int _timeImmune;
    [SerializeField] private GameObject _shieldParticle;
    [SerializeField] private ScriptableSound _shieldSound;

    public override void Execute()
    {
        List<Character> charactersToShield = new List<Character>();
        charactersToShield = CharacterManager.GetInRadius(_entity.transform.position, _shieldingRadius);

        for (int i = 0; i < Mathf.Min(charactersToShield.Count, _numOfMaxShields); i++)
        {
            charactersToShield[i].AddImmune(_timeImmune);
            var particle = GameObject.Instantiate(_shieldParticle, charactersToShield[i].gameObject.transform);
            particle.GetComponent<ParticleSystem>().startLifetime = TurnManager.TurnInterval * _timeImmune;
        }

        if (charactersToShield.Count > 0)
        {
            _shieldSound?.Play();
        }

        //if (charactersToShield.Count > _numOfMaxShields)
        //{
        //    while (charactersToShield.Count > _numOfMaxShields)
        //    {
        //        charactersToShield.RemoveAt(Random.Range(0, charactersToShield.Count));
        //    }
        //    foreach (var item in charactersToShield)
        //    {
        //        item.AddImmune(_timeImmune);
        //        var particle = GameObject.Instantiate(_shieldParticle, item.gameObject.transform);
        //        particle.GetComponent<ParticleSystem>().startLifetime = TurnManager.TurnInterval * _timeImmune;
        //    }
        //}
        //else
        //{
        //    foreach (var item in charactersToShield)
        //    {
        //        item.AddImmune(_timeImmune);
        //        var particle = GameObject.Instantiate(_shieldParticle, item.gameObject.transform);
        //        particle.GetComponent<ParticleSystem>().startLifetime = TurnManager.TurnInterval * _timeImmune;
        //    }
        //}
    }
}
