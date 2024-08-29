using ProjectAssets.Scripts.Enemy.EnemyState;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        protected StateMachine _stateMachine;
        protected PlayerView _playerView;
        protected HealthController _healthController;
        protected EnemySetting _enemySetting;
        protected MonoBehaviour _monoBehaviour;
        protected GameObject _enemy;
        
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Collider2D _collider;

        [Inject]
        public void Construct(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public void Initialize(HealthController healthController, EnemySetting enemySetting, MonoBehaviour monoBehaviour)
        {
            _healthController = healthController;
            _healthController.SetHealth(enemySetting.Health);
            _enemySetting = enemySetting;
            _monoBehaviour = monoBehaviour;
        }

        public abstract void SetupStates();

        protected virtual void Start()
        {
            _enemy = gameObject;
            _stateMachine = new StateMachine();
            SetupStates();
            _stateMachine.Transit<ChaseState>();
        }

        protected virtual void Update()
        {
            _stateMachine.Update();
            FacePlayer();
        }

        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
        
        protected void FacePlayer()
        {
            Vector2 directionToPlayer = _playerView.transform.position - transform.position;
            
            if (directionToPlayer.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (directionToPlayer.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}