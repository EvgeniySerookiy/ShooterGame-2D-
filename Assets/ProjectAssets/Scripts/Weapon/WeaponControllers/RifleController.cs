using System.Collections;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class RifleController : WeaponController
    {
        private bool _isFiring;
        public RifleController(WeaponProvider weaponProvider, IWeaponRoot weaponRoot, MonoBehaviour monoBehaviour) : 
            base(weaponProvider, weaponRoot, monoBehaviour)
        {
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Rifle;
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
                var bullet = _bulletPoolManager._bulletPool.Get();
                var direction = _weaponView.transform.right;
                bullet.Shoot(null,direction, Damage);
                
                yield return new WaitForSeconds(FireRate);
            }
            
            _isFiring = false;
        }
    }
}