using UnityEngine;

namespace AdventureAssembly.Units
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