using System;
using ProjectAssets.Scripts.Enemy.EnemyControllers;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyFactory
    {
        private readonly EnemyProvider _enemyProvider;
        private readonly DiContainer _container;
        private readonly Transform _spawnEnemyPosition;

        public EnemyFactory(EnemyProvider enemyProvider, DiContainer container, Transform spawnEnemyPosition)
        {
            Debug.Log("EnemyFactory");
            _enemyProvider = enemyProvider;
            _container = container;
            _spawnEnemyPosition = spawnEnemyPosition;
        }
        
        public EnemyController CreateEnemy(EnemyType enemyType)
        {
            switch (enemyType)
            { 
                case EnemyType.Damn:
                    return new DamnController(_enemyProvider, _container, _spawnEnemyPosition);
                case EnemyType.Flyer:
                    return new FlyerController(_enemyProvider, _container, _spawnEnemyPosition);
                case EnemyType.Raging:
                    return new RagingController(_enemyProvider, _container, _spawnEnemyPosition);
                default:
                    throw new ArgumentException("Unknown weapon type: " + enemyType);
            }
        }
    }
}