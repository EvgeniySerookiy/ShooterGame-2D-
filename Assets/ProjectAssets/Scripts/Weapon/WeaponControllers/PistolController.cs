using System.Collections;
using ProjectAssets.Scripts.Root;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class PistolController : WeaponController
    {
        private bool _isFiring;
        private Coroutine _fireCoroutine;

        public PistolController(WeaponProvider weaponProvider, MultiRoot multiRoot, MonoBehaviour monoBehaviour) :
            base(weaponProvider, multiRoot, monoBehaviour)
        {
            Debug.Log("PistolController");
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Pistol;
        }

        public override void Fire()
        {
            if (!_isFiring)
            {
                _isFiring = true;
                _fireCoroutine = MonoBehaviour.StartCoroutine(FireCoroutine());
            }
        }

        public override void StopFire()
        {
            _isFiring = false;
            MonoBehaviour.StopCoroutine(_fireCoroutine);
        }

        private IEnumerator FireCoroutine()
        {
            _isFiring = true;

            while (_isFiring)
            {
                if (_weaponView == null)
                {
                    yield break;
                }
                
                var bullet = _bulletPoolManager.GetBulletFromPool();
                var direction = _weaponView.transform.right;
                bullet.Shoot(null, direction, Damage);

                if (_weaponView != null)
                {
                    MonoBehaviour.StartCoroutine(SetMuzzleFlash());
                }

                yield return new WaitForSeconds(FireRate);
            }
        }

        private IEnumerator SetMuzzleFlash()
        {
            if (_weaponView != null && _weaponView.SpriteMuzzleFlash != null)
            {
                _weaponView.SpriteMuzzleFlash.enabled = true;
                _weaponView.SpriteMuzzleFlash.sprite =
                    _settings.SpritesMuzzleFlash[Random.Range(0, _settings.SpritesMuzzleFlash.Length)];

                yield return new WaitForSeconds(_weaponView.GetMuzzleFlashTime());
            
                if (_weaponView != null && _weaponView.SpriteMuzzleFlash != null)
                {
                    _weaponView.SpriteMuzzleFlash.enabled = false;
                }
            }
        }
    }
}