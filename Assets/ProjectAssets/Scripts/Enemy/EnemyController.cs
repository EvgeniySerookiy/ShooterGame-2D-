using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.Health;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyController
    {
        private readonly EnemyProvider _enemyProvider;
        private EnemySetting _enemySetting;
        private EnemyStatsScaler _enemyStatsScaler = new();
        

        public EnemyController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition,
            EnemyType enemyType, CoroutineLauncher coroutineLauncher, int waveNumber)
        {
            _enemyProvider = enemyProvider;
            _enemySetting = _enemyProvider.GetEnemy(enemyType).Clone();
            
            _enemyStatsScaler.ScaleStats(_enemySetting, waveNumber);
            
            var enemyView = container.InstantiatePrefabForComponent<EnemyView>(_enemySetting.ViewPrefab, spawnEnemyPosition);
            
            var enemyHealthController = container.InstantiateComponent<HealthController>(enemyView.gameObject);
            var bloodEffectController = container.InstantiateComponent<BloodEffectController>(enemyView.gameObject);
            bloodEffectController.InjectBloodEffectParticle(_enemySetting.BloodEffectParticle);
            enemyHealthController.SetHealth(_enemySetting.Health);
            enemyHealthController.InjectBloodEffectController(bloodEffectController);
            
            enemyView.Initialize(enemyHealthController, _enemySetting, coroutineLauncher);
            
            Debug.Log(_enemySetting.Health);
            Debug.Log(_enemySetting.Damage);
            Debug.Log(_enemySetting.Speed);
        }
    }
}
