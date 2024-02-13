namespace AdventureAssembly.Units.Enemies
{
    [System.Serializable]
    public struct EnemySpawnWeight
    {
        public EnemyData EnemyData;
        public int Weight;
        public float MinSpawnTime;
        public float MaxSpawnTime;
    }
}