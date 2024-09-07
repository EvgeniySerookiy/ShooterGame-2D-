using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace ProjectAssets.Scripts.Bullets
{
    public class BulletPoolManager
    {
        public ObjectPool<Bullet> _bulletPool;
        private Transform _muzzle;
        private Bullet _bulletPrefab;
        private float _speed;
        private bool _canPenetrate;
        private bool _isEnemyShooting;

        public BulletPoolManager(Transform muzzle, Bullet bulletPrefab, float speed, bool isEnemyShooting, bool canPenetrate)
        {
            _bulletPrefab = bulletPrefab;
            _speed = speed;
            _canPenetrate = canPenetrate;
            _isEnemyShooting = isEnemyShooting;
            _muzzle = muzzle;
            _bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, defaultCapacity: 20);
        }
        
        private Bullet CreateBullet()
        {
            var bullet = Object.Instantiate(_bulletPrefab, _muzzle);
            bullet.Initialize(_speed, _canPenetrate, _isEnemyShooting);
            return bullet;
        }
        
        
        private void OnGetBullet(Bullet bullet)
        {
            bullet.Hitted += _bulletPool.Release;
            bullet.transform.parent = null;
            bullet.transform.position = _muzzle.position;
            bullet.gameObject.SetActive(true);
        }
        
        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.Hitted -= _bulletPool.Release;
            bullet.transform.parent = _muzzle;
            bullet.gameObject.SetActive(false);
        }
    }
}