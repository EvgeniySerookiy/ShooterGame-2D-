using ProjectAssets.Scripts.Enemy.Settings;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyProvider
    {
        private readonly EnemySettings _enemySettings;

        private EnemyProvider(EnemySettings enemySettings)
        {
            _enemySettings = enemySettings;
        }

        public EnemySetting GetEnemy(EnemyType enemyType)
        {
            return _enemySettings.Enemies.Find(w => w.Type == enemyType);
        }
    }
}