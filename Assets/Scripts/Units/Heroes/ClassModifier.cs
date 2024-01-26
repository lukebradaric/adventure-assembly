using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    [System.Serializable]
    public abstract class ClassModifier
    {
        [Tooltip("How many Heroes of this class type are required for this buff?")]
        [OdinSerialize] public int RequiredCount { get; protected set; }

        public abstract void Apply();
        public abstract void Remove();

        public ClassModifier GetClone()
        {
            return (ClassModifier)this.MemberwiseClone();
        }
    }
}