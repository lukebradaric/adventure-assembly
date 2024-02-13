using AdventureAssembly.Units.Characters;
using AdventureAssembly.Units.Stats;

namespace AdventureAssembly.Units.Enemies
{
    public class EnemyStats : CharacterStats
    {
        public EnemyData EnemyData { get; protected set; }
        public Enemy Enemy { get; protected set; }

        public Stat<float> GoldDropChanceBonus { get; set; } = new Stat<float>(0.0f);

        public override void Initialize(Character unit)
        {
            Enemy = (Enemy)unit;
            EnemyData = (EnemyData)unit.CharacterData;

            base.Initialize(unit);
        }

        public float GetGoldDropChance()
        {
            return EnemyData.BaseGoldDropChance + GoldDropChanceBonus.Value;
        }
    }
}