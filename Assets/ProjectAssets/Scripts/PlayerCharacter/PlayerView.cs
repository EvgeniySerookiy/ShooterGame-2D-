using System;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    public class PlayerView : MonoBehaviour
    {
        public event Action OnDie;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlayerSetting _playerSetting;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _spriteLifetime;
        
        private float _speed;
        public bool _isDead { get; private set; }
        private CameraController _cameraController;
        private PlayerWeaponController _playerWeaponController;
         
        [Inject]
        public void Construct(CameraController cameraController, PlayerWeaponController playerWeaponController)
        {
            _cameraController = cameraController;
            _playerWeaponController = playerWeaponController;
        }
        
        public void Awake()
        {
            _speed = _playerSetting.Speed;
            _healthController.SetHealth(_playerSetting.Health);
        }
        
        public void TakeDamage(float damage)
        {
            if (_isDead) return;
            
            _cameraController.ShakeCamera();
            _healthController.TakeDamage(damage);
            
            if (_healthController.Health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDie?.Invoke();
            _isDead = true;
            
            _rigidbody.velocity = Vector2.zero;
            _animator.Play("DeathAnimation");
            _rigidbody = null;
            
            _playerWeaponController.StopFire();
            _playerWeaponController.GetActiveWeaponController().SetActive(false);
            
            
            Destroy(gameObject, _spriteLifetime);
        }

        public void Move(Vector2 direction)
        {
            if (_isDead) return;
            
            if (direction != Vector2.zero)
            {
                _animator.SetBool("Run",true);
                _rigidbody.velocity = direction.normalized * _speed;
            }
            else
            {
                _animator.SetBool("Run",false);
                _rigidbody.velocity = Vector2.zero;
            }
            FlipXPlayer(direction.x);
        }
        
        
        private void FlipXPlayer(float x)
        {
            if (x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (x > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }
    }
}