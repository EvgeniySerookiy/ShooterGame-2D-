using System.Collections;
using System.Collections.Generic;
using ProjectAssets.Scripts.Buffs.Settings;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffSpawner : IInitializable
    {
        private const float CHECK_RADIUS = 0.5f;
        
        private readonly int _currentBuffCount;
        private readonly BuffFactory _buffFactory;
        private readonly List<BuffView> _activeBuffs = new();
        private readonly PlayerWeaponController _playerWeaponController;
        private readonly BuffListSettings _buffListSettings;
        private readonly Transform[] _gameBoard;
        private readonly CoroutineLauncher _coroutineLauncher;
        
        public BuffSpawner(BuffFactory buffFactory, PlayerWeaponController playerWeaponController, BuffListSettings buffListSettings, CoroutineLauncher coroutineLauncher, Transform[] gameBoard)
        {
            _gameBoard = gameBoard;
            _buffListSettings = buffListSettings;
            _buffFactory = buffFactory;
            _playerWeaponController = playerWeaponController;
            _coroutineLauncher = coroutineLauncher;
        }
        
        public void Initialize()
        {
            _coroutineLauncher.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                if (_activeBuffs.Count < _buffListSettings.MaxBuffCount)
                {
                    var buffType = GetRandomBuffType();
                    var buffPosition = GetRandomSpawnPosition();
                    
                    while (IsPositionOccupied(buffPosition))
                    {
                        buffPosition = GetRandomSpawnPosition();
                    }
                    
                    var buffView = _buffFactory.CreateBuff(buffType, buffPosition);
                    buffView.Initialize(this, buffType);
                    _activeBuffs.Add(buffView);
                }

                yield return new WaitForSeconds(_buffListSettings.BuffSpawnInterval);
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
            
            buffView.DestroyBuff();
        }
        
        private bool IsPositionOccupied(Vector3 position)
        {
            Collider2D collider = Physics2D.OverlapCircle(position, CHECK_RADIUS);
            return collider != null; 
        }

        private void UpdateDamage()
        {
            _playerWeaponController.WeaponController.IncreaseDamage();
        }
    
        private void UpdateFireRate()
        {
            _playerWeaponController.WeaponController.IncreaseFireRate();
        }
    }
}