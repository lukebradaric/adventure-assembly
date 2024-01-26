namespace AdventureAssembly.Units.Modifiers
{
    [System.Serializable]
    public abstract class UnitModifier
    {
        public abstract void Apply(Unit unit);
        public abstract void Remove(Unit unit);
    }
}