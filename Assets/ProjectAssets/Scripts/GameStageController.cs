using System.Collections;
using ProjectAssets.Scripts.Enemy;
using TMPro;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class GameStageController : MonoBehaviour
    {
        private int _currentWave = 1;
        private int _remainingEnemies;
        
        private EnemySpawner _enemySpawner;
        
        [SerializeField] private float _enemySpawnInterval;
        [SerializeField] private int _initialEnemyCount;
        [SerializeField] private int _enemyIncrement;
        [SerializeField] private float _timeWaveText;
        [SerializeField] private TextMeshProUGUI _waveText;
        
        [Inject]
        public void Construct(EnemySpawner enemySpawner)
        {
            _enemySpawner = enemySpawner;
        }

        private void Start()
        {
            StartCoroutine(WaveCycle());
        }

        private IEnumerator WaveCycle()
        {
            while (true)
            {
                ShowWaveNumber();
                
                yield return new WaitForSeconds(_timeWaveText);
                
                yield return SpawnEnemies();
                
                yield return new WaitUntil(() => _remainingEnemies == 0);
                
                _initialEnemyCount += _enemyIncrement;
                _currentWave++;
            }
        }
        
        private void ShowWaveNumber()
        {
            _waveText.text = $"Wave {_currentWave}";
            _waveText.gameObject.SetActive(true);
        }

        private IEnumerator SpawnEnemies()
        {
            _waveText.gameObject.SetActive(false);
            _remainingEnemies = _initialEnemyCount;

            yield return _enemySpawner.SpawnEnemies(_initialEnemyCount, _enemySpawnInterval, _currentWave);
        }

        public void OnEnemyKilled()
        {
            _remainingEnemies--;
        }
    }
}
