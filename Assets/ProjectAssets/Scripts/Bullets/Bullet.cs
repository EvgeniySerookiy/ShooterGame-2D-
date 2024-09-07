using System;
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
            _isEnemyShooting = isEnemyShooting;
            _canPenetrate = canPenetrate;
        }

        public void Shoot(Vector2? targetPosition, Vector2? direction, float damage)
        {
            _damage = damage;

            Vector2 finalDirection;

            if (targetPosition.HasValue)
            {
                finalDirection = (targetPosition.Value - (Vector2)transform.position).normalized;
                _spriteRenderer.color = _color;
                _rigidbody2D.velocity = finalDirection * _speed;
            }
            
            if(direction.HasValue)
            {
                finalDirection = direction.Value.normalized;
                _rigidbody2D.velocity = finalDirection * _speed;
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isEnemyShooting)
            {
                if (other.gameObject.TryGetComponent(out PlayerView playerView))
                {
                    playerView.TakeDamage(_damage);
                    Hitted?.Invoke(this);
                }
            }

            if (!_isEnemyShooting)
            {
                if (other.gameObject.TryGetComponent(out HealthController enemyHealthController))
                {
                    enemyHealthController.TakeDamage(_damage);

                    if (!_canPenetrate)
                    {
                        Hitted?.Invoke(this);
                        _rigidbody2D.velocity = Vector2.zero;
                    }
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