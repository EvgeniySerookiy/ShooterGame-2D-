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
        private ChaseState _chaseState;
        private DamnMeleeAttackState _damnMeleeAttackState;
        private StateMachine _stateMachine;
        private PlayerView _playerView;
        private HealthController _healthController;
        private EnemySetting _enemySetting;
        private MonoBehaviour _monoBehaviour;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider2D _collider;
        

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

        private void Start()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _stateMachine = new StateMachine();
            _chaseState = new ChaseState(_stateMachine, _agent, _playerView.transform, _enemySetting, _animator);
            _damnMeleeAttackState = new DamnMeleeAttackState(_stateMachine, _agent, _playerView.transform, _enemySetting, _animator, _collider, _monoBehaviour);
            _stateMachine.AddStates(_chaseState, _damnMeleeAttackState);
            _stateMachine.Transit<ChaseState>();
        }

        private void Update()
        {
            _stateMachine.Update();
            FacePlayer();
        }

        public void DestroyEnemy()
        {
            Destroy(gameObject);
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
    }
}