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
                
                yield return new WaitForSeconds(FireRate);
            }
            
            _isFiring = false;
        }
    }
}