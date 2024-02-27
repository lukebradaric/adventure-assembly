using AdventureAssembly.Core;
using AdventureAssembly.Units.Bosses;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using TinyTools.Generics;
using UnityEngine;

namespace AdventureAssembly.Units.Enemies
{
    [CreateAssetMenu(menuName = Constants.ScriptableObjectRootPath + "EnemySpawnData")]
    public class EnemySpawnData : SerializedScriptableObject
    {
        [BoxGroup("Spawning")]
        [Tooltip("How many enemies should be alive at any second of the level?")]
        [SerializeField] private AnimationCurve _spawnCurve;
        public AnimationCurve SpawnCurve => _spawnCurve;

        [BoxGroup("Enemies")]
        [OdinSerialize] public List<EnemySpawnWeight> EnemySpawns { get; protected set; } = new List<EnemySpawnWeight>();

        [BoxGroup("Enemies")]
        [OdinSerialize] public List<BossSpawn> BossSpawns { get; protected set; } = new List<BossSpawn>();

        /// <summary>
        /// Returns a list of enemies based on weight and current time.
        /// </summary>
        /// <param name="currentTime">The current time in the level.</param>
        /// <returns></returns>
        public List<EnemyData> GetEnemiesToSpawn(float currentTime, int count)
        {
            WeightedList<EnemyData> weightedList = new WeightedList<EnemyData>();

            foreach (EnemySpawnWeight spawnWeight in EnemySpawns)
            {
                if (currentTime > spawnWeight.MinSpawnTime && currentTime < spawnWeight.MaxSpawnTime)
                {
                    weightedList.Add(spawnWeight.EnemyData, spawnWeight.Weight);
                }
            }

            List<EnemyData> enemies = new List<EnemyData>();
            for (int i = 0; i < count; i++)
            {
                enemies.Add(weightedList.GetRandom());
            }

            return enemies;
        }

        /// <summary>
        /// Returns the amount of enemies that should be alive based on the current time.
        /// </summary>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        public int GetEnemySpawnCount(float currentTime)
        {
            return (int)Mathf.Round(SpawnCurve.Evaluate(currentTime));
        }
    }
}