using AdventureAssembly.Core;
using AdventureAssembly.Units.Modifiers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Classes
{
    public class ClassBuff : CloneObject<ClassBuff>
    {
        [BoxGroup("Settings")]
        [Tooltip("How many Heroes of this class type are required for this buff to be active?")]
        [OdinSerialize] public int RequiredCount { get; protected set; }

        [BoxGroup("Modifiers")]
        [Tooltip("List of modifiers that will be applied.")]
        [OdinSerialize] public List<GlobalCharacterStatModifier> GlobalCharacterModifiers { get; protected set; } = new List<GlobalCharacterStatModifier>();

        public virtual void Add()
        {
            foreach (GlobalCharacterStatModifier modifier in GlobalCharacterModifiers)
            {
                modifier.Apply();
            }
        }

        public virtual void Remove()
        {
            foreach (GlobalCharacterStatModifier modifier in GlobalCharacterModifiers)
            {
                modifier.Remove();
            }
        }

        public override void OnClone(ClassBuff obj)
        {
            base.OnClone(obj);

            // Create temporary list copy
            List<GlobalCharacterStatModifier> temp = new List<GlobalCharacterStatModifier>(obj.GlobalCharacterModifiers);

            // Set clone object list to empty
            obj.GlobalCharacterModifiers = new List<GlobalCharacterStatModifier>();

            // Clone each object from the old list into the new clone object
            foreach (GlobalCharacterStatModifier modifier in temp)
            {
                obj.GlobalCharacterModifiers.Add((GlobalCharacterStatModifier)modifier.GetClone());
            }
        }
    }
}