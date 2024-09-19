using System.Collections;
using ProjectAssets.Scripts.Root;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class ShotgunController : WeaponController
    {
        private bool _isFiring;
        private Coroutine _fireCoroutine;
        
        public ShotgunController(WeaponProvider weaponProvider, MultiRoot multiRoot, MonoBehaviour monoBehaviour) :
            base(weaponProvider, multiRoot, monoBehaviour)
        {
            Debug.Log("ShotgunController");
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Shotgun;
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

                var direction = _weaponView.transform.right;
                var rotatedDirectionRight = Quaternion.Euler(0, 0, 15) * direction;
                var rotatedDirectionLeft = Quaternion.Euler(0, 0, -15) * direction;

                var directions = new Vector2[]
                {
                    rotatedDirectionRight,
                    direction,
                    rotatedDirectionLeft
                };

                for (var i = 0; i < directions.Length; i++)
                {
                    _bulletPoolManager.GetBulletFromPool().Shoot(null, directions[i], Damage);
                }
                
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