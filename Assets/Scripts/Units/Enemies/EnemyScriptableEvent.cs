using AdventureAssembly.Core;
using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "ScriptableEvents/EnemyScriptableEvent")]
    public class EnemyScriptableEvent : ScriptableEvent<Enemy> { }
}