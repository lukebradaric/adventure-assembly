using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Input
{
    /// <summary>
    /// Manager for handling player input and translating to movement directions.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static Observable<Vector2Int> MovementVector { get; private set; } = new Observable<Vector2Int>(Vector2Int.up);

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("w"))
            {
                MovementVector.Value = Vector2Int.up;
            }

            if (UnityEngine.Input.GetKeyDown("d"))
            {
                MovementVector.Value = Vector2Int.right;
            }

            if (UnityEngine.Input.GetKeyDown("s"))
            {
                MovementVector.Value = Vector2Int.down;
            }

            if (UnityEngine.Input.GetKeyDown("a"))
            {
                MovementVector.Value = Vector2Int.left;
            }
        }
    }
}