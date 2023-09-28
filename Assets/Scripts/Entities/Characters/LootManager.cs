using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private Vector4 _spawnBounds;

    private void OnEnable()
    {
        CharacterManager.LeveledUp += OnLeveledUp;
    }

    private void OnDisable()
    {
        CharacterManager.LeveledUp -= OnLeveledUp;
    }

    private void OnLeveledUp()
    {
        Vector2Int spawnPos = GetRandomPosition();
        int count = 0;

        while (CharacterManager.TryGet(spawnPos, out Character character) && count < 100)
        {
            count++;
            spawnPos = GetRandomPosition();
        }

        Instantiate(_lootPrefab, (Vector2)spawnPos, Quaternion.identity);
    }

    private Vector2Int GetRandomPosition()
    {
        return new Vector2Int(Random.Range((int)-_spawnBounds.x, (int)_spawnBounds.y), Random.Range((int)-_spawnBounds.z, (int)_spawnBounds.w));
    }
}
