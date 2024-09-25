using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly DiContainer _container;
        
        private readonly CoroutineLauncher _coroutineLauncher;

        public EnemyFactory(EnemyProvider enemyProvider, DiContainer container, CoroutineLauncher coroutineLauncher)
        {
            _enemyProvider = enemyProvider;
            _container = container;
            _coroutineLauncher = coroutineLauncher;
        }
        
        public EnemyController CreateEnemy(EnemyType enemyType, Transform spawnEnemyPosition)
        {
            return new EnemyController(_enemyProvider, _container, spawnEnemyPosition, enemyType, _coroutineLauncher);
        }
    }
}