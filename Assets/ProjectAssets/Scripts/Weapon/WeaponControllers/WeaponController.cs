using System.Collections;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Weapon.Settings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public abstract class WeaponController
    {
        public float Damage { get; private set; }
        public float FireRate { get; private set; }
        
        private readonly WeaponProvider _weaponProvider;
        private readonly Transform _weaponRoot;
        
        protected bool _isFiring;
        protected Coroutine _fireCoroutine;
        protected CoroutineLauncher _coroutineLauncher;
        protected WeaponSetting _settings;
        protected BulletPoolManager _bulletPoolManager;
        protected WeaponView _weaponView;
        
        
        protected WeaponController(WeaponProvider weaponProvider, Transform weaponRoot, CoroutineLauncher coroutineLauncher)
        {
            _coroutineLauncher = coroutineLauncher;
            _weaponRoot = weaponRoot;
            _weaponProvider = weaponProvider;
            _settings = _weaponProvider.GetWeapon(GetWeaponType());
            _weaponView = Object.Instantiate(_settings.ViewPrefab, _weaponRoot.transform);
            _bulletPoolManager = new BulletPoolManager(
                _weaponView.Muzzle,
                _settings.BulletSetting.BulletPrefab,
                _settings.BulletSetting.BulletSpeed,
                _settings.BulletSetting.IsEnemyShooting,
                _settings.BulletSetting.СanPenetrate
            );

            Damage = _settings.Damage;
            FireRate = _settings.FireRate;
        }
        
        public void IncreaseDamage()
        {
            Damage *= _settings.DamageRatio;
        }

        public void IncreaseFireRate()
        {
            FireRate /= _settings.FireRateRatio;
        }

        public void SetActive(bool isActive)
        {
            _weaponView.gameObject.SetActive(isActive);
        }

        public abstract WeaponType GetWeaponType();

        public void Fire()
        {
            if (!_isFiring)
            {
                _isFiring = true;
                _fireCoroutine = _coroutineLauncher.StartCoroutine(FireCoroutine());
            }
        }

        public void StopFire()
        {
            _isFiring = false;
            if (_fireCoroutine != null)
            {
                _coroutineLauncher.StopCoroutine(_fireCoroutine);
            }
        }
        
        protected virtual IEnumerator FireCoroutine()
        {
            _isFiring = true;

            while (_isFiring)
            {
                if (_weaponView == null)
                {
                    yield break;
                }

                var bullet = _bulletPoolManager.GetBulletFromPool();
                var direction = _weaponView.transform.right;
                bullet.Shoot(direction, Damage);

                if (_weaponView != null)
                {
                    _coroutineLauncher.StartCoroutine(SetMuzzleFlash());
                }

                yield return new WaitForSeconds(FireRate);
            }
        }

        protected IEnumerator SetMuzzleFlash()
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
