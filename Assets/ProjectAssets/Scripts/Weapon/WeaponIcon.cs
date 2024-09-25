using UnityEngine;
using System.Collections;
using ProjectAssets.Scripts.PlayerCharacter;
using Zenject;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponIcon : MonoBehaviour
    {
        private bool _isPicked;
        
        private PlayerWeaponController _playerWeaponController;
        private CoroutineLauncher _coroutineLauncher;
        
        [SerializeField] private float respawnTime;
        [SerializeField] private WeaponType _weaponType;

        [Inject]
        public void Construct(PlayerWeaponController playerWeaponController, CoroutineLauncher coroutineLauncher)
        {
            _coroutineLauncher = coroutineLauncher;
            _playerWeaponController = playerWeaponController;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out Player playerView)) return;
            
            if (_playerWeaponController.WeaponType == _weaponType || !_isPicked)
            {
                HandleWeaponPickup();
            }
        }

        private void HandleWeaponPickup()
        {
            if (!_isPicked)
            {
                _isPicked = true;
                _playerWeaponController.SwitchWeapons(_weaponType);
            }
    
            gameObject.SetActive(false);
            _coroutineLauncher.StartCoroutine(RespawnIcon());
        }

        private IEnumerator RespawnIcon()
        {
            yield return new WaitForSeconds(respawnTime);
            gameObject.SetActive(true);
            _isPicked = false;
        }
    }
}