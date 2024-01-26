using UnityEngine;

namespace AdventureAssembly.Units.Tests
{
    public class DefaultStatModifier : StatModifier<float>
    {
        [SerializeField] private float _value;

        public override float Process(float value)
        {
            value += _value;
            return value;
        }
    }
}