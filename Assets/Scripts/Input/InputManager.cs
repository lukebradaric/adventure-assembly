using AdventureAssembly.Core;
using UnityEngine;

namespace AdventureAssembly.Input
{
    public class InputManager : MonoBehaviour
    {
        public static Direction MovementDirection { get; private set; } = Direction.Up;
        public static Vector2Int MovementVector { get; private set; } = Vector2Int.up;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("w"))
            {
                MovementDirection = Direction.Up;
                MovementVector = Vector2Int.up;
            }

            if (UnityEngine.Input.GetKeyDown("d"))
            {
                MovementDirection = Direction.Right;
                MovementVector = Vector2Int.right;
            }

            if (UnityEngine.Input.GetKeyDown("s"))
            {
                MovementDirection = Direction.Down;
                MovementVector = Vector2Int.down;
            }

            if (UnityEngine.Input.GetKeyDown("a"))
            {
                MovementDirection = Direction.Left;
                MovementVector = Vector2Int.left;
            }
        }
    }
}