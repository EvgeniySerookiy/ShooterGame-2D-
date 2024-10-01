using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.GoogleImporter;
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
        private ConfigImportsMenu _configImportsMenu;
        
        public EnemyController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition,
            EnemyType enemyType, CoroutineLauncher coroutineLauncher, int waveNumber, ConfigImportsMenu configImportsMenu)
        {
            _coroutineLauncher = coroutineLauncher;
            _spawnEnemyPosition = spawnEnemyPosition;
            _container = container;
            _waveNumber = waveNumber;
            _enemyProvider = enemyProvider;
            _configImportsMenu = configImportsMenu;
            _enemySetting = _enemyProvider.GetEnemy(enemyType).Clone();
            InitializeEnemySettings();
        }
        
        public async void InitializeEnemySettings()
        { 
            _configImportsMenu.UpdateEnemySettingFromJson(_enemySetting);
            Debug.Log("______");
            Debug.Log(_enemySetting.Health);
            Debug.Log(_enemySetting.Damage);
            Debug.Log(_enemySetting.Speed);
            Debug.Log(_enemySetting.HealthRatio);
            Debug.Log(_enemySetting.DamageRatio);
            Debug.Log(_enemySetting.SpeedRatio);
            Debug.Log("______");
            
            _enemyStatsScaler.ScaleStats(_enemySetting, _waveNumber);
            Debug.Log("______");
            Debug.Log(_enemySetting.Health);
            Debug.Log(_enemySetting.Damage);
            Debug.Log(_enemySetting.Speed);
            Debug.Log(_enemySetting.HealthRatio);
            Debug.Log(_enemySetting.DamageRatio);
            Debug.Log(_enemySetting.SpeedRatio);
            Debug.Log("______");
            
            var enemyView = _container.InstantiatePrefabForComponent<EnemyView>(_enemySetting.ViewPrefab, _spawnEnemyPosition);
            
            var enemyHealthController = _container.InstantiateComponent<HealthController>(enemyView.gameObject);
            var bloodEffectController = _container.InstantiateComponent<BloodEffectController>(enemyView.gameObject);
            bloodEffectController.InjectHealthController(enemyHealthController, _enemySetting.BloodEffectParticle);
            enemyHealthController.SetHealth(_enemySetting.Health);
            
            enemyView.Initialize(enemyHealthController, _enemySetting, _coroutineLauncher);
        }
    }
}
