namespace AdventureAssembly.Units.Stats
{
    [System.Serializable]
    public abstract class StatProcess
    {
        public abstract object Process(object value);
    }

    public abstract class StatProcess<T> : StatProcess
    {
        public override object Process(object value)
        {
            return Process((T)value);
        }

        public abstract T Process(T value);
    }
}