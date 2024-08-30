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
        
        [SerializeField] private NavMeshAgent _agent;
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
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Start()
        {
            _stateMachine = new StateMachine();
            var chaseState = new ChaseState(_stateMachine, _playerView.transform, _enemySetting, _animator, _agent);
            //var damnMeleeAttackState = new DamnMeleeAttackState(_stateMachine, _agent, _playerView.transform, _enemySetting, _animator, _collider, _monoBehaviour);
            _stateMachine.AddStates(chaseState);
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