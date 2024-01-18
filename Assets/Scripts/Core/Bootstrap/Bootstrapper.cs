using UnityEngine;

namespace AdventureAssembly.Core.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            GameObject corePrefab = Resources.Load<GameObject>("Core/CorePrefab");
            GameObject coreGameObject = Instantiate(corePrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(coreGameObject);
        }
    }
}
