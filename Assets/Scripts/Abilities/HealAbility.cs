using System.Collections.Generic;
using TinyTools.ScriptableSounds;
using UnityEngine;

public class HealAbility : Ability
{
    [SerializeField] private int _healAmount;
    [SerializeField] private int _healRadius;
    [SerializeField] private GameObject _healParticles;
    [SerializeField] private ScriptableSound _healSound;

    public override void Execute()
    {
        //TODO: Character Manager GetNearbyCharactersInRadius <- Returns Lists
        List<Character> inRadius = new List<Character>();
        inRadius = CharacterManager.GetInRadius(_entity.transform.position, _healRadius);

        foreach (Character character in inRadius)
        {
            character.Heal(_healAmount);
            var particles = GameObject.Instantiate(_healParticles, character.transform);
            particles.GetComponent<ParticleSystem>().startLifetime = TurnManager.TurnInterval;
        }

        if (inRadius.Count > 0)
        {
            _healSound?.Play();
        }
    }
}
