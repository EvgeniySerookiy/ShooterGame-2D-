using System.Collections;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class PistolController : WeaponController
    {
        private bool _isFiring;

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
                MonoBehaviour.StartCoroutine(FireCoroutine());
            }
        }

        public override void StopFire()
        {
            _isFiring = false;
        }

        private IEnumerator FireCoroutine()
        {
            _isFiring = true;

            while (_isFiring)
            {
                var bullet = _bulletPoolManager.GetBulletFromPool();
                var direction = _weaponView.transform.right;
                bullet.Shoot(null, direction, Damage);

                MonoBehaviour.StartCoroutine(SetMuzzleFlash());

                yield return new WaitForSeconds(FireRate);
            }
        }

        private IEnumerator SetMuzzleFlash()
        {
            _weaponView.SpriteMuzzleFlash.enabled = true;
            _weaponView.SpriteMuzzleFlash.sprite =
                _settings.SpritesMuzzleFlash[Random.Range(0, _settings.SpritesMuzzleFlash.Length)];

            yield return new WaitForSeconds(_weaponView.GetMuzzleFlashTime());

            _weaponView.SpriteMuzzleFlash.enabled = false;
        }
    }
}