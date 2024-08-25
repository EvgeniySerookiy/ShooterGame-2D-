using UnityEngine;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlayerSetting _playerSetting;
        private float _speed;
        
        public void Awake()
        {
            _speed = _playerSetting.Speed;
        }

        public void Move(Vector2 direction)
        {
            if (direction != Vector2.zero)
            {
                _rigidbody.velocity = direction.normalized * _speed;
            }
            else
            {
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