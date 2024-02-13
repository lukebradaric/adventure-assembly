using AdventureAssembly.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureAssembly.Units
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "LevelData")]
    public class LevelData : SerializedScriptableObject
    {
        [BoxGroup("Scene")]
        [SerializeField] private SceneReference _sceneReference;
        public SceneReference SceneReference => _sceneReference;
    }
}