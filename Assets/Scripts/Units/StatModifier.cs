namespace AdventureAssembly.Units
{
    public abstract class StatModifier<T>
    {
        public abstract T Process(T value);
    }
}