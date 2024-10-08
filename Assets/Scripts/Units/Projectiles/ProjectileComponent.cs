﻿using AdventureAssembly.Core;

namespace AdventureAssembly.Units.Projectiles
{
    public abstract class ProjectileComponent : CloneObject<ProjectileComponent>
    {
        protected Projectile _projectile;

        public virtual void Initialize(Projectile projectile)
        {
            _projectile = projectile;
        }

        public abstract void OnEnable();
        public abstract void OnDisable();

        public virtual void OnDrawGizmos() { }
    }
}