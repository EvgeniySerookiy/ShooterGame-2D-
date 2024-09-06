using System;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using ProjectAssets.Scripts.Waters;
using UnityEngine;

namespace ProjectAssets.Scripts.Bullets
{
    public class BulletEnemy : MonoBehaviour
    {
        public event Action<BulletEnemy> Hitted;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private EnemySetting _enemySetting;
        private float _damage;

        public void Initialize(EnemySetting enemySetting)
        {
            _enemySetting = enemySetting;
        }

        public void Shoot(Vector2 targetPosition, float damage)
        {
            _damage = damage;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            _rigidbody2D.velocity = direction * _enemySetting.AttackSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerView playerView))
            {
                playerView.TakeDamage(_damage);
                Hitted?.Invoke(this);
            }
            
            if (other.gameObject.TryGetComponent(out Water water))
            {
                Hitted?.Invoke(this);
                _rigidbody2D.velocity = Vector2.zero;
            }
        }
    }
}