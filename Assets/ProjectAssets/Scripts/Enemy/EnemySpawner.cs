using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemySpawner
    {
        private Transform[] _spawnEnemyPositions;
        private EnemyFactory _enemyFactory;
        
        public EnemySpawner(EnemyFactory enemyFactory, Transform[] spawnEnemyPositions)
        {
            _enemyFactory = enemyFactory;
            _spawnEnemyPositions = spawnEnemyPositions;
        }

        public IEnumerator SpawnEnemies(int enemyCount, float enemySpawnInterval)
        {
            while (enemyCount > 0)
            {
                _enemyFactory.CreateEnemy(GetRandomEnemyType(), GetRandomSpawnEnemyPosition());

                yield return new WaitForSeconds(enemySpawnInterval);

                enemyCount--;
            }
        }

        private EnemyType GetRandomEnemyType()
        {
            return (EnemyType)Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
        }

        private Transform GetRandomSpawnEnemyPosition()
        {
            return _spawnEnemyPositions[Random.Range(0, _spawnEnemyPositions.Length)];
        }
    }
}