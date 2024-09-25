using ProjectAssets.Scripts.Enemy.Settings;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyStatsScaler
    {
        public void ScaleStats(EnemySetting enemySetting, int waveNumber)
        {
            enemySetting.SetHealth(enemySetting.Health * (1 + waveNumber * enemySetting.HealthRatio));
            enemySetting.SetDamage(enemySetting.Damage * (1 + waveNumber * enemySetting.DamageRatio));
            enemySetting.SetSpeed(enemySetting.Speed * (1 + waveNumber * enemySetting.SpeedRatio));
        }
    }
}