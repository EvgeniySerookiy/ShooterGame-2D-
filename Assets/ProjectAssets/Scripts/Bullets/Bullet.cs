using System;
using ProjectAssets.Scripts.Bullets.Settings;
using ProjectAssets.Scripts.Waters;
using UnityEngine;

namespace ProjectAssets.Scripts.Bullets
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> Hitted;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private float _damage;
        
        private BulletSetting _bulletSetting;

        public void Initialize(BulletSetting bulletSetting)
        {
            _bulletSetting = bulletSetting;
        }

        public void Shoot(Vector2 direction, float damage)
        {
            _damage = damage;
            _rigidbody2D.velocity = direction * _bulletSetting.BulletSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out HealthController enemyHealthController))
            {
                enemyHealthController.TakeDamage(_damage);

                if (!_bulletSetting.СanPenetrate)
                {
                    Hitted?.Invoke(this);
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }
            
            if (other.gameObject.TryGetComponent(out Water water))
            {
                Hitted?.Invoke(this);
                _rigidbody2D.velocity = Vector2.zero;
            }
        }
    }
}