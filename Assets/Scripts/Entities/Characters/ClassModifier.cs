using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassModifier
{
    //public Stat stat;
    public string statName;

    public List<ClassModifierData> modifierData = new List<ClassModifierData>();

    private ClassModifierData _currentModifierData = null;
    private int _currentCount = 0;

    public void AddCount(int value)
    {
        _currentCount += value;
        if (_currentCount < 0) _currentCount = 0;
        ClassModifierData newData = GetModifierData(_currentCount);

        // If that class modifier data is already applied, return
        if (_currentModifierData == newData)
        {
            return;
        }

        // If there is currently a modifier applied, remove it
        if (_currentModifierData != null)
        {
            // TODO: Implement stat manager
            //stat.AddValue(-_currentModifierData.value);
        }

        if (newData != null)
        {
            // TODO: Implement stat manager
            //stat.AddValue(newData.value);
        }

        _currentModifierData = newData;
    }

    // Returns the nearest modifier data based on the count
    private ClassModifierData GetModifierData(int count)
    {
        ClassModifierData newData = null;

        foreach (ClassModifierData data in modifierData)
        {
            if (count >= data.count)
            {
                newData = data;
            }
        }

        return newData;
    }
}
