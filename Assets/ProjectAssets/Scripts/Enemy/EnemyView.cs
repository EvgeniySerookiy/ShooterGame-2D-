using ProjectAssets.Scripts.Enemy.EnemyState;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using MeleeAttackState = ProjectAssets.Scripts.Enemy.EnemyState.MeleeAttackState;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private ChaseState _chaseState;
        private MeleeAttackState _meleeAttackState;
        private StateMachine _stateMachine;
        private PlayerView _playerView;
        private HealthController _healthController;
        //private float _damage;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        

        [Inject]
        public void Construct(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public void Initialize(float health, HealthController healthController)
        {
            _healthController = healthController;
            _healthController.SetHealth(health);
        }

        private void Start()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _stateMachine = new StateMachine();
            _chaseState = new ChaseState(_stateMachine, _agent, _playerView.transform);
            _meleeAttackState = new MeleeAttackState(_stateMachine, _agent, _playerView.transform);
            _stateMachine.AddStates(_chaseState, _meleeAttackState);
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
        
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.TryGetComponent(out HealthController enemyHealthController))
        //    {
        //        enemyHealthController.TakeDamage(_damage);
        //        
        //        //if (!_bulletSetting.СanPenetrate)
        //        //{
        //        //    Hitted?.Invoke(this);
        //        //    _rigidbody2D.velocity = Vector2.zero;
        //        //}
        //    }
        //}
    }
}