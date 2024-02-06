using AdventureAssembly.Units.Heroes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureAssembly.Units.Classes
{
    public class ClassManager : MonoBehaviour
    {
        // The count of how many Heroes there are for each class type
        public static Dictionary<ClassData, int> ClassCount { get; private set; } = new Dictionary<ClassData, int>();

        // Dictionary of the class buffs currently applied for each class
        public static Dictionary<ClassData, ClassBuff> ClassBuffs { get; private set; } = new Dictionary<ClassData, ClassBuff>();

        // Dictionary of ClassBuff instances created for each class
        public static Dictionary<ClassData, List<ClassBuff>> ClassBuffInstances { get; private set; } = new Dictionary<ClassData, List<ClassBuff>>();

        public static Action<ClassData> ClassDataAdded;
        public static Action<ClassData> ClassDataRemoved;

        public static Action<ClassData, int> ClassCountChanged;

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

        /// <summary>
        /// Returns the current amount of Heroes the player has of a Class.
        /// </summary>
        /// <param name="classData">The Class to check</param>
        public static int GetClassCount(ClassData classData)
        {
            if (!ClassCount.ContainsKey(classData))
            {
                return 0;
            }

            return ClassCount[classData];
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

                // Invoke event when class data is added
                ClassDataAdded?.Invoke(classData);
            }

            // Invoke class count changed event
            ClassCountChanged?.Invoke(classData, ClassCount[classData]);

            UpdateClassBuffs(classData, ClassCount[classData]);
        }

        public static void RemoveClass(ClassData classData)
        {
            if (!ClassCount.ContainsKey(classData))
            {
                Debug.LogError("Tried to remove a class that has not been added.");
                return;
            }

            ClassCount[classData]--;
            UpdateClassBuffs(classData, ClassCount[classData]);

            // Invoke class count changed event
            ClassCountChanged?.Invoke(classData, ClassCount[classData]);

            // If class count hit zero, remove dict entry
            if (ClassCount[classData] == 0)
            {
                ClassCount.Remove(classData);

                // Invoke event when class data is completely removed
                ClassDataRemoved(classData);
            }
        }

        // Update the buffs applied from each class buff
        private static void UpdateClassBuffs(ClassData classData, int currentCount)
        {
            ClassBuff classBuff = GetClassBuff(classData, currentCount);

            // If no buff found and new buff is null, return
            if (!ClassBuffs.ContainsKey(classData) && classBuff == null)
            {
                //Debug.Log("No class update.");
                return;
            }

            // If no buff found and new buff is valid, add clone
            if (!ClassBuffs.ContainsKey(classData) && classBuff != null)
            {
                //Debug.Log("Adding a brand new buff.");

                // Add buff to dictionary
                ClassBuffs.Add(classData, classBuff);

                // Apply buff effect
                classBuff.Apply();

                return;
            }

            // If buff is found and new buff is null, remove old
            if (ClassBuffs.ContainsKey(classData) && classBuff == null)
            {
                //Debug.Log("Completely removing a buff");

                // Remove buff effects
                ClassBuffs[classData].Remove();

                // Remove buff from dictionary
                ClassBuffs.Remove(classData);
                return;
            }

            // If buff is found and new buff does not match, remove old, add new
            if (ClassBuffs[classData].RequiredCount != classBuff.RequiredCount)
            {
                //Debug.Log("Updating an existing buff");

                // Remove the old buff effects
                ClassBuffs[classData].Remove();

                // Update the class buff value in the dictionary
                ClassBuffs[classData] = classBuff;

                // Apply the new class buff
                classBuff.Apply();

                return;
            }
        }

        // Get the ClassBuff that should be applied based on the current class count
        private static ClassBuff GetClassBuff(ClassData classData, int currentCount)
        {
            ClassBuff classBuff = null;

            // Create cloned instances for each class buff so they can be added/removed with .Equals()
            if (!ClassBuffInstances.ContainsKey(classData))
            {
                List<ClassBuff> instances = new List<ClassBuff>();

                foreach (ClassBuff buff in classData.Buffs)
                {
                    instances.Add(buff.GetClone());
                }

                ClassBuffInstances.Add(classData, instances);
            }

            foreach (ClassBuff buff in ClassBuffInstances[classData])
            {
                if (currentCount >= buff.RequiredCount)
                {
                    classBuff = buff;
                }
            }

            return classBuff;
        }
    }
}