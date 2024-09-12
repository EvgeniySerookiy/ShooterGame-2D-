using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Weapon.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public abstract class WeaponController
    {
        public float Damage { get; set; }
        public float FireRate { get; set; }
        public MonoBehaviour MonoBehaviour { get; }
     
        private readonly WeaponProvider _weaponProvider;
        private readonly MultiRoot _multiRoot;
        
        protected WeaponSetting _settings;
        protected BulletPoolManager _bulletPoolManager;
        protected WeaponView _weaponView;


        protected WeaponController (WeaponProvider weaponProvider, MultiRoot multiRoot, MonoBehaviour monoBehaviour)
        {
            MonoBehaviour = monoBehaviour;
            Debug.Log("WeaponController");
            _multiRoot = multiRoot;
            _weaponProvider = weaponProvider;
            _settings = _weaponProvider.GetWeapon(GetWeaponType());
            _weaponView = Object.Instantiate(_settings.ViewPrefab, _multiRoot.GetRootForWeapon());
            _bulletPoolManager = new BulletPoolManager(_weaponView.Muzzle, 
                _settings.BulletSetting.BulletPrefab, _settings.BulletSetting.BulletSpeed, 
                _settings.BulletSetting.IsEnemyShooting, _settings.BulletSetting.СanPenetrate);
            Damage = _settings.Damage;
            FireRate = _settings.FireRate;
        }
        
        public void SetActive(bool isActive)
        {
            _weaponView.gameObject.SetActive(isActive);
        }
        
        public abstract WeaponType GetWeaponType();
        
        public abstract void Fire();
        public abstract void StopFire();
    }
}