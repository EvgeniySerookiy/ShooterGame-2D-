using System;
using ProjectAssets.Scripts.Health;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    public class Player : MonoBehaviour
    {
        public event Action OnDying;
        
        private float _speed;
        private CameraController _cameraController;
        private PlayerWeaponController _playerWeaponController;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlayerSetting _playerSetting;
        [SerializeField] private Animator _animator;
        [field:SerializeField] public float _spriteLifetime { get; private set; }
        [field:SerializeField] public HealthController HealthController { get; private set; }
        public bool IsDead { get; private set; }
         
         
        [Inject]
        public void Construct(CameraController cameraController, PlayerWeaponController playerWeaponController)
        {
            _cameraController = cameraController;
            _playerWeaponController = playerWeaponController;
        }
        
        public void Awake()
        {
            _speed = _playerSetting.Speed;
            HealthController.SetHealth(_playerSetting.Health);
            
            HealthController.OnHealthChanged += HandleDamageTaken;
            
        }

        private void HandleDamageTaken()
        {
            if (IsDead) return;
            
            _cameraController.ShakeCamera();
            
            if (HealthController.Health <= 0)
            {
                Dying();
            }
        }
        

        private void Dying()
        {
            OnDying?.Invoke();
            IsDead = true;
            Time.timeScale = 0;
        }

        public void PlayDeathAnimation()
        {
            _animator.Play("DeathAnimation");
        }

        public void Died()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody = null;
            
            _playerWeaponController.StopFire();
            _playerWeaponController.WeaponController.SetActive(false);
            
            Destroy(gameObject, _spriteLifetime);
        }

        public void Continue()
        {
            IsDead = false;
            HealthController.SetHealth(_playerSetting.Health);
            Time.timeScale = 1;
        }

        public void Move(Vector2 direction)
        {
            if (IsDead) return;
            
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

        private void OnDestroy()
        {
            HealthController.OnHealthChanged -= HandleDamageTaken;
        }
    }
}