using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Heroes
{
    public class ClassManager : MonoBehaviour
    {
        // The count of how many Heroes there are for each class type
        public static Dictionary<ClassData, int> ClassCount { get; private set; } = new Dictionary<ClassData, int>();

        // Dictionary of the class modifiers currently applied for each class
        public static Dictionary<ClassData, ClassModifier> ClassModifiers { get; private set; } = new Dictionary<ClassData, ClassModifier>();

        // Dictionary of classmodifier instances created for each class
        public static Dictionary<ClassData, List<ClassModifier>> ClassModifierInstances { get; private set; } = new Dictionary<ClassData, List<ClassModifier>>();

        public static void AddClassesByHeroData(HeroData heroData)
        {
            foreach (ClassData classData in heroData.ClassData)
            {
                AddClass(classData);
            }
        }

        public static void RemoveClassesByHeroData(HeroData heroData)
        {
            foreach (ClassData classData in heroData.ClassData)
            {
                RemoveClass(classData);
            }
        }

        // When a new class is added from a hero
        public static void AddClass(ClassData classData)
        {
            if (ClassCount.ContainsKey(classData))
            {
                ClassCount[classData]++;
            }
            else
            {
                ClassCount.Add(classData, 1);
            }


            UpdateClassModifiers(classData, ClassCount[classData]);
        }

        public static void RemoveClass(ClassData classData)
        {
            if (!ClassCount.ContainsKey(classData))
            {
                Debug.LogError("Tried to remove a class that has not been added.");
                return;
            }

            ClassCount[classData]--;
            UpdateClassModifiers(classData, ClassCount[classData]);

            // If class count hit zero, remove dict entry
            if (ClassCount[classData] == 0)
            {
                ClassCount.Remove(classData);
            }
        }

        // Update the modifiers applied from each class buff
        private static void UpdateClassModifiers(ClassData classData, int currentCount)
        {
            ClassModifier classModifier = GetClassModifier(classData, currentCount);

            // If no modifier found and new modifier is null, return
            if (!ClassModifiers.ContainsKey(classData) && classModifier == null)
            {
                //Debug.Log("No class update.");
                return;
            }

            // If no modifier found and new modifier is valid, add clone
            if (!ClassModifiers.ContainsKey(classData) && classModifier != null)
            {
                //Debug.Log("Adding a brand new modifier.");
                ClassModifiers.Add(classData, classModifier);
                classModifier.Apply();
                return;
            }

            // If modifier is found and new modifier is null, remove old
            if (ClassModifiers.ContainsKey(classData) && classModifier == null)
            {
                //Debug.Log("Completely removing a modifier");
                ClassModifiers[classData].Remove();
                ClassModifiers.Remove(classData);
                return;
            }

            // If modifier is found and new modifier does not match, remove old, add new
            if (ClassModifiers[classData].RequiredCount != classModifier.RequiredCount)
            {
                //Debug.Log("Updating an existing modifier");
                ClassModifiers[classData].Remove();
                ClassModifiers[classData] = classModifier;
                classModifier.Apply();
                return;
            }
        }

        // Get the ClassModifier that should be applied based on the current class count
        private static ClassModifier GetClassModifier(ClassData classData, int currentCount)
        {
            ClassModifier classModifier = null;

            // Create cloned instances for each class modifier so they can be added/removed with .Equals()
            if (!ClassModifierInstances.ContainsKey(classData))
            {
                List<ClassModifier> instances = new List<ClassModifier>();

                foreach (ClassModifier modifier in classData.Modifiers)
                {
                    instances.Add(modifier.GetClone());
                }

                ClassModifierInstances.Add(classData, instances);
            }

            foreach (ClassModifier modifier in ClassModifierInstances[classData])
            {
                if (currentCount >= modifier.RequiredCount)
                {
                    classModifier = modifier;
                }
            }

            return classModifier;
        }
    }
}