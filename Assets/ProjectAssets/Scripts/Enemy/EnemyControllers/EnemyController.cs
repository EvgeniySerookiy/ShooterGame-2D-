using ProjectAssets.Scripts.Enemy.Settings;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy.EnemyControllers
{
    public abstract class EnemyController
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly EnemySetting _enemySetting;
        
        protected EnemyController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition)
        {
            _enemyProvider = enemyProvider;
            _enemySetting = _enemyProvider.GetEnemy(GetEnemyType());
            
            var enemyView = container.InstantiatePrefabForComponent<EnemyView>(_enemySetting.ViewPrefab, spawnEnemyPosition);
            
            var enemyHealthController = enemyView.gameObject.AddComponent<EnemyHealthController>();
            enemyHealthController.SetHealth(_enemySetting.Health);
            
            enemyView.Initialize(_enemySetting.Health, enemyHealthController);
        }
        
        public abstract EnemyType GetEnemyType();
    }
}