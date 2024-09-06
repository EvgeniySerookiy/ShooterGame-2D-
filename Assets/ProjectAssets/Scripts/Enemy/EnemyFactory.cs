using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly DiContainer _container;
        private readonly Transform _spawnEnemyPosition;
        private readonly MonoBehaviour _monoBehaviour;

        public EnemyFactory(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition, MonoBehaviour monoBehaviour)
        {
            _enemyProvider = enemyProvider;
            _container = container;
            _spawnEnemyPosition = spawnEnemyPosition;
            _monoBehaviour = monoBehaviour;
        }
        
        public EnemyController CreateEnemy(EnemyType enemyType)
        {
            return new EnemyController(_enemyProvider, _container, _spawnEnemyPosition, enemyType, _monoBehaviour);
        }
    }
}