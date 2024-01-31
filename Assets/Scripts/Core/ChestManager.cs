using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Core
{
    public class ChestManager : MonoBehaviour
    {
        [Space]
        [Header("Events")]
        [Tooltip("Event called when the player levels up.")]
        [SerializeField] private VoidScriptableEvent _leveledUpScriptableEvent;
        [Tooltip("Event called when a new chest has spawned.")]
        [SerializeField] private VoidScriptableEvent _chestSpawnedScriptableEvent;

        [Space]
        [Header("Prefabs")]
        [SerializeField] private GameObject _chestPrefab;

        [Space]
        [Header("Settings")]
        [SerializeField] private Vector4 _spawnBounds;

        private void OnEnable()
        {
            _leveledUpScriptableEvent.VoidEvent += OnLevelUp;
        }

        private void OnDisable()
        {
            _leveledUpScriptableEvent.VoidEvent -= OnLevelUp;
        }

        private void OnLevelUp()
        {
            // Calculate random spawn position from spawn bounds
            //Vector2 spawnPosition = new Vector2(Random.Range((int)_spawnBounds.x, (int)_spawnBounds.y), Random.Range((int)_spawnBounds.z, (int)_spawnBounds.w));
            Vector2 spawnPosition = GridManager.GetRandomEmptyPosition(_spawnBounds);

            Instantiate(_chestPrefab, spawnPosition, Quaternion.identity);

            _chestSpawnedScriptableEvent?.Invoke();
        }
    }
}