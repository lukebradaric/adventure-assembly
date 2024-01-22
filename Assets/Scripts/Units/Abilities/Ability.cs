using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    public abstract class Ability
    {
        [Tooltip("How often should this ability occur?")]
        [SerializeField] private int _turns;
    }
}