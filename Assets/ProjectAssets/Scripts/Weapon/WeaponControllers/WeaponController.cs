using System;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Weapon.Settings;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public abstract class WeaponController : IDisposable
    {
        public float Damage { get; set; }
        public float FireRate { get; set; }
        public MonoBehaviour MonoBehaviour { get; }
     
        private readonly WeaponProvider _weaponProvider;
        protected ObjectPool<Bullet> BulletPool;
        private readonly WeaponSetting _settings;
        private readonly IWeaponRoot _weaponRoot;
        protected WeaponView _weaponView;


        protected WeaponController (WeaponProvider weaponProvider, IWeaponRoot weaponRoot, MonoBehaviour monoBehaviour)
        {
            MonoBehaviour = monoBehaviour;
            Debug.Log("WeaponController");
            _weaponRoot = weaponRoot;
            _weaponProvider = weaponProvider;
            _settings = _weaponProvider.GetWeapon(GetWeaponType());
            _weaponView = Object.Instantiate(_settings.ViewPrefab, _weaponRoot.Root);
            BulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, defaultCapacity: 100);
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            Damage = _settings.Damage;
            FireRate = _settings.FireRate;
        }

        private Bullet CreateBullet()
        {
            var bullet = Object.Instantiate(_settings.BulletSetting.BulletPrefab, _weaponView.Muzzle);
            bullet.Initialize(_settings.BulletSetting);
            return bullet;
        }
        
        
        private void OnGetBullet(Bullet bullet)
        {
            bullet.Hitted += BulletPool.Release;
            bullet.transform.parent = null;
            bullet.transform.position = _weaponView.Muzzle.position;
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.Hitted -= BulletPool.Release;
            bullet.transform.parent = _weaponView.Muzzle;
            bullet.gameObject.SetActive(false);
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
            BulletPool.Dispose();
        }
    }
}