using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Space]
    [Header("Components")]
    [SerializeField] private Camera _camera;

    [Space]
    [Header("Settings")]
    [SerializeField] private float _minCameraSize = 6;
    [SerializeField] private float _cameraSizeBuffer = 2;
    [SerializeField] private float _lerpSpeed = 3.5f;
    [SerializeField] private float _sizeLerpSpeed = 0.125f;
    [SerializeField] private float _zOffset = -10;

    private float _desiredCameraSize;
    private Vector3 _desiredPosition;

    private void OnEnable()
    {
        TurnManager.TurnUpdate += OnTurnUpdate;
    }

    private void OnDisable()
    {
        TurnManager.TurnUpdate -= OnTurnUpdate;
    }

    private void OnTurnUpdate()
    {
        Bounds characterBounds = CharacterManager.GetBounds();
        _desiredCameraSize = Mathf.Max(Mathf.Max(characterBounds.size.x, characterBounds.size.y) + _cameraSizeBuffer, _minCameraSize);

        _desiredPosition = CharacterManager.GetCenter();
        _desiredPosition.z = _zOffset;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, _lerpSpeed * Time.deltaTime);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _desiredCameraSize, _sizeLerpSpeed * Time.deltaTime);
    }
}
