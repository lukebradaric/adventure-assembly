using AdventureAssembly.Core;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Input
{
    public class InputManager : Singleton<InputManager>
    {
        public Direction MovementDirection { get; private set; } = Direction.Up;
        public Vector2 MovementVector { get; private set; } = Vector2.up;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("w"))
            {
                MovementDirection = Direction.Up;
                MovementVector = Vector2.up;
            }

            if (UnityEngine.Input.GetKeyDown("d"))
            {
                MovementDirection = Direction.Right;
                MovementVector = Vector2.right;
            }

            if (UnityEngine.Input.GetKeyDown("s"))
            {
                MovementDirection = Direction.Down;
                MovementVector = Vector2.down;
            }

            if (UnityEngine.Input.GetKeyDown("a"))
            {
                MovementDirection = Direction.Left;
                MovementVector = Vector2.left;
            }
        }
    }
}