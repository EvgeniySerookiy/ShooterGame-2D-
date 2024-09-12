using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _enemySpawnInterval;
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private Transform[] _spawnEnemyPositions;
        
        private EnemyFactory _enemyFactory;
        
        [Inject]
        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (_maxEnemyCount != 0)
            {
                EnemyType randomEnemyType = EnemyType.Flyer;
                
                _enemyFactory.CreateEnemy(GetRandomEnemyType(), GetRandomSpawnEnemyPosition());
                
                yield return new WaitForSeconds(_enemySpawnInterval);

                _maxEnemyCount--;
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