using UnityEngine;

namespace AdventureAssembly.Core.Extensions
{
    public static class DirectionExtensions
    {
        public static Vector2 ToVector2(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2.up;
                case Direction.Right:
                    return Vector2.right;
                case Direction.Down:
                    return Vector2.down;
                case Direction.Left:
                    return Vector2.left;
                default:
                    return Vector2.zero;
            }
        }

        public static Vector3 ToVector3(this Direction direction)
        {
            Vector2 vector2 = ToVector2(direction);
            return new Vector3(vector2.x, vector2.y, 0);
        }
    }
}