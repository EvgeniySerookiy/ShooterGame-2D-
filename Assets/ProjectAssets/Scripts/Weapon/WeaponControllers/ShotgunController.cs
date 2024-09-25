using System.Collections;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class ShotgunController : WeaponController
    {
        public ShotgunController(WeaponProvider weaponProvider, Transform weaponRoot, CoroutineLauncher coroutineLauncher) :
            base(weaponProvider, weaponRoot, coroutineLauncher)
        {
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Shotgun;
        }
        
        protected override IEnumerator FireCoroutine()
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
                    _bulletPoolManager.GetBulletFromPool().Shoot(directions[i], Damage);
                }
                
                if (_weaponView != null)
                {
                    _coroutineLauncher.StartCoroutine(SetMuzzleFlash());
                }

                yield return new WaitForSeconds(FireRate);
            }
        }
    }
}