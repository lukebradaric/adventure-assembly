using UnityEngine;

namespace AdventureAssembly.Core.Extensions
{
    public static class FloatExtensions
    {
        public static bool Chance(this float value)
        {
            return Random.value < value;
        }
    }
}