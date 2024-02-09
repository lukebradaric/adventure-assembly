using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units.Interactables
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
        [SerializeField] private InteractableUnit _chestUnitPrefab;

        [Space]
        [Header("Settings")]
        [SerializeField] private Vector4 _spawnBounds;
        [Tooltip("What is the max amount of chests that can be active at once?")]
        [SerializeField] private int _maxConcurrentChestCount = 5;

        private int _currentChestCount = 0;

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
            if (_currentChestCount >= _maxConcurrentChestCount)
            {
                return;
            }

            // Calculate random spawn position from spawn bounds
            Vector2Int spawnPosition = GridManager.GetRandomEmptyPosition(_spawnBounds);

            InteractableUnit chestUnit = Instantiate(_chestUnitPrefab, (Vector2)spawnPosition, Quaternion.identity);
            chestUnit.Initialize(spawnPosition);
            chestUnit.Destroyed += OnChestDestroyed;
            _currentChestCount++;

            _chestSpawnedScriptableEvent?.Invoke();
        }

        private void OnChestDestroyed()
        {
            _currentChestCount--;
        }
    }
}