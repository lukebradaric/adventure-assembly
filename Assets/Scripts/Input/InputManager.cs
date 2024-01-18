using AdventureAssembly.Core;
using TinyTools.Generics;

namespace AdventureAssembly.Input
{
    public class InputManager : Singleton<InputManager>
    {
        public Direction MovementDirection { get; private set; } = Direction.Up;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown("w"))
            {
                MovementDirection = Direction.Up;
            }

            if (UnityEngine.Input.GetKeyDown("d"))
            {
                MovementDirection = Direction.Right;
            }

            if (UnityEngine.Input.GetKeyDown("s"))
            {
                MovementDirection = Direction.Down;
            }

            if (UnityEngine.Input.GetKeyDown("a"))
            {
                MovementDirection = Direction.Left;
            }
        }
    }
}