using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Enemy.EnemyState;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private PlayerView _playerView;
        private HealthController _healthController;
        private EnemySetting _enemySetting;
        private MonoBehaviour _monoBehaviour;
        private Bullet _bulletPrefab;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Transform _muzzle;

        [Inject]
        public void Construct(PlayerView playerView, Bullet bulletPrefab)
        {
            _playerView = playerView;
            _bulletPrefab = bulletPrefab;
        }
        
        public void Initialize(HealthController healthController, EnemySetting enemySetting, MonoBehaviour monoBehaviour)
        {
            _healthController = healthController;
            _healthController.SetHealth(enemySetting.Health);
            _enemySetting = enemySetting;
            _monoBehaviour = monoBehaviour;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            var chaseState = new ChaseState(_stateMachine, _playerView.transform, _enemySetting, _animator, 
                _agent, _healthController);
            var attackState = new AttackState(_stateMachine, _agent, _playerView.transform, _enemySetting, 
                _animator, _collider, _monoBehaviour, _healthController);
            var deathState = new DeathState(_stateMachine, _animator, _agent, _spriteRenderer);
            var firingState = new FiringState(_stateMachine, _bulletPrefab, _muzzle, 
                _playerView, _enemySetting, _animator, _healthController, _monoBehaviour);
            
            _stateMachine.AddStates(chaseState, attackState, deathState, firingState);
            _stateMachine.Transit<ChaseState>();
        }

        private void Update()
        {
            _stateMachine.Update();
            FacePlayer();
        }
        
        private void FacePlayer()
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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerView playerView))
            {
                playerView.TakeDamage(_enemySetting.Damage);
            }
        }
    }
}