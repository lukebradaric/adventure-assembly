using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private int _dropRange;

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
        Bounds bounds = CharacterManager.GetBounds();
        Vector2 spawnPos = new Vector2(Random.Range((int)bounds.min.x, (int)bounds.max.x + 1), Random.Range((int)bounds.min.y, (int)bounds.max.y + 1));
        spawnPos += new Vector2(Random.Range(-_dropRange, _dropRange + 1), Random.Range(-_dropRange, _dropRange + 1));

        Instantiate(_lootPrefab, spawnPos, Quaternion.identity);
    }
}
