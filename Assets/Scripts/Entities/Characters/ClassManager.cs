using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour
{
    private static HashSet<Class> _currentClasses = new HashSet<Class>();

    private static bool Contains(Class cl)
    {
        foreach (Class clas in _currentClasses)
        {
            if (clas.name == cl.name)
            {
                return true;
            }
        }

        return false;
    }

    private static bool TryGet(Class cl, out Class clas)
    {
        clas = null;
        foreach (Class c in _currentClasses)
        {
            if (c.name == cl.name)
            {
                clas = c;
                return true;
            }
        }

        return false;
    }

    public static void AddClass(Class cl)
    {
        // If class not in hashset, add
        if (!Contains(cl))
        {
            _currentClasses.Add(cl.GetClone());
        }

        if (TryGet(cl, out Class currentClass))
        {
            foreach (ClassModifier modifier in currentClass.modifiers)
            {
                modifier.AddCount(1);
            }
        }
    }

    public static void RemoveClass(Class cl)
    {
        if (!Contains(cl))
        {
            return;
        }

        if (TryGet(cl, out cl))
        {
            foreach (ClassModifier modifier in cl.modifiers)
            {
                modifier.AddCount(-1);
            }
        }
    }
}
