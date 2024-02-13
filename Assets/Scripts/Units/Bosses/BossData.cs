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
        [BoxGroup("General", Order = 0)]
        [OdinSerialize] public Boss Prefab { get; private set; }

        public void Create(Vector2Int position)
        {
            Boss boss = Instantiate(Prefab, (Vector2)position, Quaternion.identity);
            boss.Initialize(this, position);
        }
    }
}