using UnityEngine;
using System.Collections;
using ProjectAssets.Scripts.PlayerCharacter;
using Zenject;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponIcon : MonoBehaviour
    {
        [SerializeField] private float respawnTime;
        [SerializeField] private WeaponType _weaponType;

        private PlayerWeaponController _playerWeaponController;
        private MonoBehaviour _monoBehaviour;
        private bool _isPicked = false;

        [Inject]
        public void Construct(PlayerWeaponController playerWeaponController, MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
            _playerWeaponController = playerWeaponController;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerView playerView))
            {
                if (!_isPicked)
                {
                    _isPicked = true;
                    gameObject.SetActive(false);
                    _playerWeaponController.SwitchWeapons(_weaponType);
                    _monoBehaviour.StartCoroutine(RespawnIcon());
                }
            }
        }

        private IEnumerator RespawnIcon()
        {
            yield return new WaitForSeconds(respawnTime);
            gameObject.SetActive(true);
            _isPicked = false;
        }
    }
}