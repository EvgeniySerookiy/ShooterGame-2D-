using System.Collections;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class ShotgunController : WeaponController
    {
        private bool _isFiring;
        public ShotgunController(WeaponProvider weaponProvider, IWeaponRoot weaponRoot, MonoBehaviour monoBehaviour) : 
            base(weaponProvider, weaponRoot, monoBehaviour)
        {
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Shotgun;
        }

        public override void Fire()
        {
            if (!_isFiring)
            {
                MonoBehaviour.StartCoroutine(FireCoroutine());
            }
        }
        
        private IEnumerator FireCoroutine()
        {
            _isFiring = true;
            
            while (_isFiring)
            {
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
                    _bulletPoolManager._bulletPool.Get().Shoot(null,directions[i], Damage);
                }
                
                MonoBehaviour.StartCoroutine(SetMuzzleFlash());
                
                yield return new WaitForSeconds(FireRate);
            }
            
            _isFiring = false;
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