using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private Vector4 _spawnBounds;
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
        Vector2Int spawnPos = GetRandomPosition();

        while (CharacterManager.TryGet(spawnPos, out Character character))
        {
            spawnPos = GetRandomPosition();
        }

        Instantiate(_lootPrefab, (Vector2)spawnPos, Quaternion.identity);
    }

    private Vector2Int GetRandomPosition()
    {
        return new Vector2Int(Random.Range((int)-_spawnBounds.x, (int)_spawnBounds.y), Random.Range((int)-_spawnBounds.z, (int)_spawnBounds.w));
        //Vector2Int spawnPo
        //Bounds bounds = CharacterManager.GetBounds();
        //Vector2Int spawnPos = new Vector2Int(Random.Range((int)bounds.min.x, (int)bounds.max.x + 1), Random.Range((int)bounds.min.y, (int)bounds.max.y + 1));
        //spawnPos += new Vector2Int(Random.Range(-_dropRange, _dropRange + 1), Random.Range(-_dropRange, _dropRange + 1));
        //return spawnPos;
    }
}
