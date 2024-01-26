using UnityEngine;

namespace AdventureAssembly.Units.Tests
{
    public class DefaultStatModifier : StatModifier<float>
    {
        public DefaultStatModifier(float value = 0f)
        {
            _value = value;
        }

        [SerializeField] private float _value;

        public override float Process(float value)
        {
            value += _value;
            return value;
        }
    }
}