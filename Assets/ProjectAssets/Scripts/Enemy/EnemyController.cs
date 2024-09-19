using ProjectAssets.Scripts.Enemy.Settings;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyController
    {
        public float Health { get; private set; }
        public float Damage { get; private set; }
        public float Speed { get; private set; }
        
        private readonly EnemyProvider _enemyProvider;
        private readonly EnemySetting _enemySetting;

        public EnemyController(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition,
            EnemyType enemyType, MonoBehaviour monoBehaviour)
        {
            _enemyProvider = enemyProvider;
            _enemySetting = _enemyProvider.GetEnemy(enemyType);
            
            var enemyView = container.InstantiatePrefabForComponent<EnemyView>(_enemySetting.ViewPrefab, spawnEnemyPosition);
            
            var enemyHealthController = container.InstantiateComponent<HealthController>(enemyView.gameObject);
            enemyHealthController.SetHealth(_enemySetting.Health);
            enemyHealthController.SetHieEffectPrefab(_enemySetting.BloodEffectParticle);
            
            enemyView.Initialize(enemyHealthController, _enemySetting, monoBehaviour);
        }
    }
}
