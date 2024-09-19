using System.Collections;
using System.Collections.Generic;
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
        private List<BuffView> _activeBuffs = new();
        private PlayerWeaponController _playerWeaponController;

        [Inject]
        public void Construct(BuffFactory buffFactory, PlayerWeaponController playerWeaponController)
        {
            _buffFactory = buffFactory;
            _playerWeaponController = playerWeaponController;
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                if (_activeBuffs.Count < _maxBuffCount)
                {
                    var buffType = GetRandomBuffType();
                    var buffView = _buffFactory.CreateBuff(buffType, GetRandomSpawnPosition());
                    buffView.Initialize(this, buffType);
                    _activeBuffs.Add(buffView);
                }

                yield return new WaitForSeconds(_buffSpawnInterval);
            }
        }

        private BuffType GetRandomBuffType()
        {
            return (BuffType)Random.Range(0, System.Enum.GetValues(typeof(BuffType)).Length);
        }

        private Vector3 GetRandomSpawnPosition()
        {
            float x = Random.Range(_gameBoard[0].position.x, _gameBoard[1].position.x);
            float y = Random.Range(_gameBoard[0].position.y, _gameBoard[1].position.y);
            return new Vector3(x, y, 0);
        }

        public void RemoveBuff(BuffView buffView)
        {
            _activeBuffs.Remove(buffView);
        
            if (buffView.BuffType == BuffType.Damage)
            {
                UpdateDamage();
            }
        
            if (buffView.BuffType == BuffType.FireRate)
            {
                UpdateFireRate();
            }
        
            Destroy(buffView.gameObject);
        }

        private void UpdateDamage()
        {
            _playerWeaponController.GetActiveWeaponController().IncreaseDamage();
        }
    
        private void UpdateFireRate()
        {
            _playerWeaponController.GetActiveWeaponController().IncreaseFireRate();
        }
    }
}