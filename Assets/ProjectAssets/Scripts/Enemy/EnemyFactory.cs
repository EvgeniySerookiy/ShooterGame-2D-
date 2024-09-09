using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly DiContainer _container;
        private readonly Transform[] _spawnEnemyPositions;
        private readonly MonoBehaviour _monoBehaviour;

        public EnemyFactory(EnemyProvider enemyProvider, DiContainer container, Transform[] spawnEnemyPositions, MonoBehaviour monoBehaviour)
        {
            _enemyProvider = enemyProvider;
            _container = container;
            _spawnEnemyPositions = spawnEnemyPositions;
            _monoBehaviour = monoBehaviour;
        }
        
        public EnemyController CreateEnemy(EnemyType enemyType)
        {
            return new EnemyController(_enemyProvider, _container, GetRandomSpawnEnemyPosition(), enemyType, _monoBehaviour);
        }

        private Transform GetRandomSpawnEnemyPosition()
        {
            return _spawnEnemyPositions[Random.Range(0, _spawnEnemyPositions.Length)];
        }
    }
}