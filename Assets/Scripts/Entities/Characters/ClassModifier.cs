using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassModifier
{
    [Tooltip("The name of the stat in code.")]
    public Stat stat;
    [Tooltip("Does this apply to all characters? Or just characters of this class.")]
    public bool isGlobal = true;

    public List<ClassModifierData> modifierData = new List<ClassModifierData>();
    private ClassModifierInstance _currentModifier = null;

    private int _count = 0;
    private Class _class;

    public void SetClass(Class cl)
    {
        _count = 0;
        _class = cl;
    }

    public void RegisterCharacter()
    {
        _count++;
        UpdateModifiers();
    }

    public void UnregisterCharacter()
    {
        _count--;
        UpdateModifiers();
    }

    public void UpdateModifiers()
    {
        ClassModifierInstance newData = GetModifierData(_count);

        if (newData == _currentModifier)
        {
            return;
        }

        if (_currentModifier != null)
        {
            if (isGlobal)
            {
                CharacterManager.RemoveGlobalModifier(_currentModifier);
            }
            else
            {
                CharacterManager.RemoveClassModifier(_class, _currentModifier);
            }
        }

        if (newData != null)
        {
            if (isGlobal)
            {
                CharacterManager.AddGlobalModifier(newData);
            }
            else
            {
                CharacterManager.AddClassModifier(_class, newData);
            }
        }

        _currentModifier = newData;
    }

    // Returns the nearest modifier data based on the count
    private ClassModifierInstance GetModifierData(int count)
    {
        ClassModifierData newData = null;

        foreach (ClassModifierData data in modifierData)
        {
            if (count >= data.count)
            {
                newData = data;
            }
        }

        if (newData == null)
        {
            return null;
        }

        if (newData?.value == _currentModifier?.value)
        {
            return _currentModifier;
        }

        ClassModifierInstance instance = new ClassModifierInstance();
        instance.statName = stat.name;
        instance.value = newData.value;
        return instance;
    }
}
