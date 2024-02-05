using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Classes
{
    [System.Serializable]
    public abstract class ClassModifier
    {
        [BoxGroup("Settings")]
        [Tooltip("How many Heroes of this class type are required for this buff to be active?")]
        [OdinSerialize] public int RequiredCount { get; protected set; }

        public abstract void Apply();
        public abstract void Remove();

        public ClassModifier GetClone()
        {
            return (ClassModifier)this.MemberwiseClone();
        }
    }
}