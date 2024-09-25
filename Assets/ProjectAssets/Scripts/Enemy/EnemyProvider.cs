using System.Collections.Generic;
using System.Linq;
using ProjectAssets.Scripts.Enemy.Settings;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyProvider
    {
        private readonly Dictionary<EnemyType, EnemySetting>  _enemyListSettings;

        private EnemyProvider(EnemyListSettings enemyListSettings)
        {
            _enemyListSettings = enemyListSettings
                .Enemies
                .ToDictionary(e => e.Type, e => e);
        }

        public EnemySetting GetEnemy(EnemyType enemyType)
        {
            return _enemyListSettings[enemyType];
        }
    }
}