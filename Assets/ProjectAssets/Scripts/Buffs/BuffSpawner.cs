using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffSpawner : MonoBehaviour
    {
        [SerializeField] private float _buffSpawnInterval;
        [SerializeField] private int _maxBuffCount;
        [SerializeField] private Transform[] _gameBoard;

        private int _currentBuffCount;
        private BuffFactory _buffFactory;

        [Inject]
        public void Construct(BuffFactory buffFactory)
        {
            _buffFactory = buffFactory;
        }
        
        private BuffType GetRandomBuffType()
        {
            return (BuffType)Random.Range(0, Enum.GetValues(typeof(BuffType)).Length);
        }

        private void Update()
        {
            if (_currentBuffCount <= _maxBuffCount)
            {
                StartCoroutine(Spawn());
            }
        }
        
        private IEnumerator Spawn()
        {
            while (_maxBuffCount != 0)
            {
                _buffFactory.CreateBuff(GetRandomBuffType(), GetRandomSpawnPosition());

                _currentBuffCount++;
                
                yield return new WaitForSeconds(_buffSpawnInterval);

                _maxBuffCount--;
            }
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            float x = Random.Range(_gameBoard[0].position.x, _gameBoard[1].position.x);
            float y = Random.Range(_gameBoard[0].position.y, _gameBoard[1].position.y);
            return new Vector3(x, y, 0);
        }
    }
}