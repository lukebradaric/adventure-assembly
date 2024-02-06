namespace AdventureAssembly.Units
{
    [System.Serializable]
    public abstract class ProjectileComponent
    {
        protected Projectile _projectile;

        public virtual void Initialize(Projectile projectile)
        {
            _projectile = projectile;
        }

        public abstract void OnEnable();
        public abstract void OnDisable();

        public ProjectileComponent GetClone()
        {
            return (ProjectileComponent)this.MemberwiseClone();
        }
    }
}