using ProjectAssets.Scripts.GoogleImporter;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly DiContainer _container;
        private readonly CoroutineLauncher _coroutineLauncher;
        private readonly ConfigImportsMenu _configImportsMenu;

        public EnemyFactory(EnemyProvider enemyProvider, DiContainer container, CoroutineLauncher coroutineLauncher, ConfigImportsMenu configImportsMenu)
        {
            _enemyProvider = enemyProvider;
            _container = container;
            _coroutineLauncher = coroutineLauncher;
            _configImportsMenu = configImportsMenu;
        }
        
        public EnemyController CreateEnemy(EnemyType enemyType, Transform spawnEnemyPosition, int waveNumber)
        {
            return new EnemyController(_enemyProvider, _container, spawnEnemyPosition, enemyType, _coroutineLauncher, waveNumber, _configImportsMenu);
        }
    }
}