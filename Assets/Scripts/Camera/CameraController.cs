using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Space]
    [Header("Settings")]
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private float _zOffset;

    private void Update()
    {
        Vector3 newPosition = CharacterManager.GetCenter();
        newPosition.z = _zOffset;

        transform.position = Vector3.Lerp(transform.position, newPosition, _lerpSpeed * Time.deltaTime);
    }
}
