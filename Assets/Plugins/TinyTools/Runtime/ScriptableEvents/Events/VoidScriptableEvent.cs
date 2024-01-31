using System;
using UnityEngine;

namespace TinyTools.ScriptableEvents
{
    [CreateAssetMenu(menuName = "TinyTools/ScriptableEvents/VoidScriptableEvent")]
    public class VoidScriptableEvent : ScriptableEvent<Type>
    {
        public void Invoke()
        {
            base.Invoke(null);
        }
    }
}
