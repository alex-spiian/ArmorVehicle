namespace ArmorVehicle
{
    public class EnemyAiData
    {
        public EnemyConfig EnemyConfig { get; }
        public IHealthHandler Target { get; }

        public EnemyAiData(EnemyConfig enemyConfig, IHealthHandler target)
        {
            EnemyConfig = enemyConfig;
            Target = target;
        }
    }
}