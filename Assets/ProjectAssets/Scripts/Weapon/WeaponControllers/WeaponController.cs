using System;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Weapon.Settings;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public abstract class WeaponController : IDisposable
    {
        public float Damage { get; set; }
        public float FireRate { get; set; }
        public MonoBehaviour MonoBehaviour { get; }
     
        private readonly WeaponProvider _weaponProvider;
        private readonly WeaponSetting _settings;
        private readonly IWeaponRoot _weaponRoot;
        protected BulletPoolManager _bulletPoolManager;
        protected WeaponView _weaponView;


        protected WeaponController (WeaponProvider weaponProvider, IWeaponRoot weaponRoot, MonoBehaviour monoBehaviour)
        {
            MonoBehaviour = monoBehaviour;
            Debug.Log("WeaponController");
            _weaponRoot = weaponRoot;
            _weaponProvider = weaponProvider;
            _settings = _weaponProvider.GetWeapon(GetWeaponType());
            _weaponView = Object.Instantiate(_settings.ViewPrefab, _weaponRoot.Root);
            _bulletPoolManager = new BulletPoolManager(_weaponView.Muzzle, 
                _settings.BulletSetting.BulletPrefab, _settings.BulletSetting.BulletSpeed, 
                _settings.BulletSetting.IsEnemyShooting, _settings.BulletSetting.СanPenetrate);
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            Damage = _settings.Damage;
            FireRate = _settings.FireRate;
        }
        
        public void SetActive(bool isActive)
        {
            _weaponView.gameObject.SetActive(isActive);
        }
        
        public abstract WeaponType GetWeaponType();
        
        public abstract void Fire();
        
        public void Dispose()
        {
            Object.Destroy(_weaponView.gameObject);
            _bulletPoolManager._bulletPool.Dispose();
        }
    }
}