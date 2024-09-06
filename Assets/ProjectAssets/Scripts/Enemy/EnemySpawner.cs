using System.Collections;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyFactory _enemyFactory;

        [Inject]
        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public void Start()
        {
            //EnemyType randomEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                
            // Создание врага
            //_enemyFactory.CreateEnemy(randomEnemyType);
            //StartCoroutine(Spawn());
            //StartCoroutine(SpawnEnemies());
            StartCoroutine(SpawnFiring());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                // Случайный тип врага
                //EnemyType randomEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                EnemyType randomEnemyType = EnemyType.Flyer;
                // Создание врага
                _enemyFactory.CreateEnemy(randomEnemyType);
                
                // Ожидание 1 секунды перед следующим спавном
                yield return new WaitForSeconds(1f);
            }
        }
        
        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                // Случайный тип врага
                //EnemyType randomEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                EnemyType randomEnemyType = EnemyType.Damn;
                // Создание врага
                _enemyFactory.CreateEnemy(randomEnemyType);
                
                // Ожидание 1 секунды перед следующим спавном
                yield return new WaitForSeconds(1f);
            }
        }
        
        private IEnumerator SpawnFiring()
        {
            while (true)
            {
                // Случайный тип врага
                //EnemyType randomEnemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
                EnemyType randomEnemyType = EnemyType.Raging;
                // Создание врага
                _enemyFactory.CreateEnemy(randomEnemyType);
                
                // Ожидание 1 секунды перед следующим спавном
                yield return new WaitForSeconds(100f);
            }
        }
    }
}