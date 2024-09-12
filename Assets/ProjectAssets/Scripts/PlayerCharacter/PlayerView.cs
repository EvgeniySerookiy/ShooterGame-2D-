using Cinemachine;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlayerSetting _playerSetting;
        [SerializeField] public HealthController _healthController;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Animator _animator;
        
        private float _speed;
        private CameraController _cameraController;
        
        [Inject]
        public void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
        }
        
        public void Awake()
        {
            _speed = _playerSetting.Speed;
            _healthController.SetHealth(_playerSetting.Health);
        }
        
        public void TakeDamage(float damage)
        {
            _cameraController.ShakeCamera();
            _healthController.TakeDamage(damage);
        }
        
        public void Move(Vector2 direction)
        {
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