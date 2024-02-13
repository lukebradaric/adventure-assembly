using AdventureAssembly.Core;
using AdventureAssembly.Units.Enemies;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace AdventureAssembly.Units.Bosses
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + nameof(BossData))]
    public class BossData : EnemyData
    {
        new public Boss Create(Vector2Int position)
        {
            Boss boss = (Boss)Instantiate(Prefab, (Vector2)position, Quaternion.identity);
            boss.Initialize(this, position);
            return boss;
        }
    }
}