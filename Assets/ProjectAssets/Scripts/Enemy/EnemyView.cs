using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Enemy.EnemyState;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.Health;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    { 
        private bool _isDead;
        
        private StateMachine _stateMachine;
        private Player _player;
        private HealthController _healthController;
        private GameStageController _gameStageController;
        private EnemySetting _enemySetting;
        private CoroutineLauncher _coroutineLauncher;
        private Bullet _bulletPrefab;
        
        [SerializeField] private float _spriteLifetime;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Transform _muzzle;
        

        [Inject]
        public void Construct(Player player, Bullet bulletPrefab, GameStageController gameStageController)
        {
            _player = player;
            _bulletPrefab = bulletPrefab;
            _gameStageController = gameStageController;
        }
        
        public void Initialize(HealthController healthController, EnemySetting enemySetting, CoroutineLauncher coroutineLauncher)
        {
            _healthController = healthController;
            _healthController.SetHealth(enemySetting.Health);
            _enemySetting = enemySetting;
            _coroutineLauncher = coroutineLauncher;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _healthController.OnHealthChanged += HandleDamageTaken;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            var chaseState = new ChaseState(_stateMachine, _player, _enemySetting, _animator, 
                _agent, _healthController);
            var attackState = new AttackState(_stateMachine, _agent, _player, _enemySetting, 
                _animator, _collider, _coroutineLauncher, _healthController);
            var deathState = new DeathState(_stateMachine, _animator, _agent, _spriteRenderer);
            var firingState = new FiringState(_stateMachine, _bulletPrefab, _muzzle, 
                _player, _enemySetting, _animator, _healthController, _coroutineLauncher);
            var idleState = new IdleState(_stateMachine, _animator, _agent);
            
            _stateMachine.AddStates(chaseState, attackState, deathState, firingState, idleState);
            _stateMachine.Transit<ChaseState>();
        }

        private void Update()
        {
            _stateMachine.Update();
            FacePlayer();
        }
        
        private void HandleDamageTaken()
        {
            if (_healthController.Health <= 0)
            {
                Die();
            }
        }
        
        private void FacePlayer()
        {
            if (_isDead || _player == null) return;

            var directionToPlayer = _player.transform.position - transform.position;
            _spriteRenderer.flipX = directionToPlayer.x < 0;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player playerView))
            {
                playerView.HealthController.TakeDamage(_enemySetting.Damage);
            }
        }
        
        private void Die()
        {
            if(_isDead) return;
            
            _isDead = true;
            
            Destroy(_collider);
            
            _gameStageController.OnEnemyKilled();
            Destroy(gameObject, _spriteLifetime);
        }

        private void OnDestroy()
        {
            _healthController.OnHealthChanged -= HandleDamageTaken;
        }
    }
}