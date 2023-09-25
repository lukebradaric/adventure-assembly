//using System.Collections.Generic;
//using UnityEngine;

//public class StatManager
//{
//    private static List<Stat> _stats = new List<Stat>();

//    public static float GetValue(string statName)
//    {
//        foreach (Stat stat in _stats)
//        {
//            if (stat.name == statName)
//            {
//                return stat.value;
//            }
//        }

//        return 1f;
//    }a

//    // Try to get the value of a stat based on name
//    public static float GetValue(Stat stat)
//    {
//        return GetValue(stat.name);
//    }

//    // Try to add value to a stat based on name
//    public static void AddValue(Stat stat, float value)
//    {
//        foreach (Stat s in _stats)
//        {
//            if (s.name == stat.name)
//            {
//                s.value += value;
//                Debug.Log($"Stat updated: {s.name} : {s.value}");
//                return;
//            }
//        }

//        // If stat wasn't already in list, add new
//        Debug.Log($"Stat added: {stat.name} : {value}");
//        stat = stat.GetClone();
//        stat.value = value;
//        _stats.Add(stat);
//    }
//}
