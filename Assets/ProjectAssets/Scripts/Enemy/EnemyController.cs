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
        private int _waveNumber;
        private DiContainer _container;
        private Transform _spawnEnemyPosition;
        private CoroutineLauncher _coroutineLauncher;
        
        public EnemyController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition,
            EnemyType enemyType, CoroutineLauncher coroutineLauncher, int waveNumber)
        {
            _coroutineLauncher = coroutineLauncher;
            _spawnEnemyPosition = spawnEnemyPosition;
            _container = container;
            _waveNumber = waveNumber;
            _enemyProvider = enemyProvider;
            _enemySetting = _enemyProvider.GetEnemy(enemyType).Clone();
            InitializeEnemySettings();
        }
        
        public void InitializeEnemySettings()
        { 
            _enemySetting.UpdateEnemySettingsFromRemote();
            
            _enemyStatsScaler.ScaleStats(_enemySetting, _waveNumber);
            
            var enemyView = _container.InstantiatePrefabForComponent<EnemyView>(_enemySetting.ViewPrefab, _spawnEnemyPosition);
            
            var enemyHealthController = _container.InstantiateComponent<HealthController>(enemyView.gameObject);
            var bloodEffectController = _container.InstantiateComponent<BloodEffectController>(enemyView.gameObject);
            bloodEffectController.InjectHealthController(enemyHealthController, _enemySetting.BloodEffectParticle);
            enemyHealthController.SetHealth(_enemySetting.Health);
            
            enemyView.Initialize(enemyHealthController, _enemySetting, _coroutineLauncher);
        }
    }
}
