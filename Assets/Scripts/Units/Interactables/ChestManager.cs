using TinyTools.ScriptableEvents;
using UnityEngine;

namespace AdventureAssembly.Units.Interactables
{
    public class ChestManager : MonoBehaviour
    {
        [Space]
        [Header("Events")]
        [Tooltip("Event called when a new chest has spawned.")]
        [SerializeField] private GameScriptableEvent _onChestSpawned;

        [Space]
        [Header("Prefabs")]
        [SerializeField] private InteractableUnit _chestUnitPrefab;

        [Space]
        [Header("Settings")]
        [Tooltip("What is the max amount of chests that can be active at once?")]
        [SerializeField] private int _maxConcurrentChestCount = 5;

        private int _currentChestCount = 0;

        public void OnPlayerLeveledUp(GameEventData gameEventData)
        {
            if (_currentChestCount >= _maxConcurrentChestCount)
            {
                return;
            }

            // Calculate random spawn position from spawn bounds
            Vector2Int spawnPosition = GridManager.GetRandomEmptyPosition();

            InteractableUnit chestUnit = Instantiate(_chestUnitPrefab, (Vector2)spawnPosition, Quaternion.identity);
            chestUnit.Initialize(spawnPosition);
            chestUnit.Destroyed += OnChestDestroyed;
            _currentChestCount++;

            _onChestSpawned?.Invoke(this, chestUnit);
        }

        private void OnChestDestroyed()
        {
            _currentChestCount--;
        }
    }
}