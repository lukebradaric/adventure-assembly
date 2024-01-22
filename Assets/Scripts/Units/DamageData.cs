namespace AdventureAssembly.Units
{
    public class DamageData
    {
        public DamageData(int baseValue)
        {
            BaseValue = baseValue;
        }

        public int BaseValue { get; private set; }

        public int Value { get; set; }
        public bool IsCritical { get; set; }
    }
}