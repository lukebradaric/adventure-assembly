﻿using AdventureAssembly.Units.Heroes;
using UnityEngine;

namespace AdventureAssembly.Units.Abilities
{
    [System.Serializable]
    public abstract class Ability
    {
        [Tooltip("How often should this ability occur? Measured in seconds.")]
        [SerializeField] private float _baseSpeed;

        // Current ticks until ability is executed
        protected float _currentTime;

        // The hero this ability is on
        protected Hero _hero;

        public void Initialize(Hero hero)
        {
            _hero = hero;
            _currentTime = _baseSpeed;
        }

        public void OnUpdate(float time)
        {
            _currentTime -= time;

            if (_currentTime <= 0)
            {
                Execute();
                _currentTime = _hero.Stats.GetAbilitySpeed(_baseSpeed);
            }
        }

        protected abstract void Execute();

        public Ability GetClone()
        {
            return (Ability)this.MemberwiseClone();
        }
    }
}