using System.Collections;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class PistolController : WeaponController
    {
        private bool _isFiring;

        public PistolController(WeaponProvider weaponProvider, IWeaponRoot weaponRoot, MonoBehaviour monoBehaviour) : 
            base(weaponProvider, weaponRoot, monoBehaviour)
        {
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Pistol;
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
                var bullet = BulletPool.Get();
                var direction = _weaponView.transform.right;
                bullet.Shoot(direction, Damage);
                
                yield return new WaitForSeconds(FireRate);
            }
            
            _isFiring = false;
        }
    }
}