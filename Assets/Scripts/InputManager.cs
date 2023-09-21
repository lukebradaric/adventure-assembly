using TinyTools.AutoLoad;
using UnityEngine;

[AutoLoad]
public class InputManager : MonoBehaviour
{
    public static Vector2Int Axis = Vector2Int.right;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Axis = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S))
            Axis = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A))
            Axis = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D))
            Axis = Vector2Int.right;
    }
}
