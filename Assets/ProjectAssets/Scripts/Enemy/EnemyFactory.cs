using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly DiContainer _container;
        
        private readonly MonoBehaviour _monoBehaviour;

        public EnemyFactory(EnemyProvider enemyProvider, DiContainer container, MonoBehaviour monoBehaviour)
        {
            _enemyProvider = enemyProvider;
            _container = container;
            _monoBehaviour = monoBehaviour;
        }
        
        public EnemyController CreateEnemy(EnemyType enemyType, Transform spawnEnemyPosition)
        {
            return new EnemyController(_enemyProvider, _container, spawnEnemyPosition, enemyType, _monoBehaviour);
        }
    }
}