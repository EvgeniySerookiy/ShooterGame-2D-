using System;
using ProjectAssets.Scripts.Health;
using ProjectAssets.Scripts.PlayerCharacter;
using ProjectAssets.Scripts.Waters;
using UnityEngine;

namespace ProjectAssets.Scripts.Bullets
{
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> Hitted;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _color;

        private float _damage;
        private float _speed;
        private bool _canPenetrate;
        private bool _isEnemyShooting;

        public void Initialize(float speed, bool canPenetrate, bool isEnemyShooting)
        {
            _speed = speed;
            _canPenetrate = canPenetrate;
            _isEnemyShooting = isEnemyShooting;
        }

        public void Shoot(Vector2 direction, float damage)
        {
            _damage = damage;
            _rigidbody2D.velocity = direction.normalized * _speed;

            if (_isEnemyShooting)
            {
                _spriteRenderer.color = _color;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            bool hitSomething = false;

            if (_isEnemyShooting && other.gameObject.TryGetComponent(out Player playerView))
            {
                playerView.HealthController.TakeDamage(_damage);
                hitSomething = true;
            }
            
            if (! _isEnemyShooting && other.gameObject.TryGetComponent(out HealthController enemyHealthController))
            {
                enemyHealthController.TakeDamage(_damage);
                hitSomething = true;

                if (!_canPenetrate)
                {
                    StopBullet();
                }
            }
            
            if (other.gameObject.TryGetComponent(out Water _))
            {
                StopBullet();
                hitSomething = true;
            }

            if (hitSomething)
            {
                Hitted?.Invoke(this);
            }
        }

        private void StopBullet()
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }
}