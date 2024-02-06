using UnityEngine;

namespace AdventureAssembly.Units.Stats
{
    /// <summary>
    /// Increases the value of a float stat by the provided value.
    /// </summary>
    public class FloatAddStatProcess : StatProcess<float>
    {
        [SerializeField] private float _value = 0f;

        public override float Process(float value)
        {
            return value + _value;
        }
    }
}