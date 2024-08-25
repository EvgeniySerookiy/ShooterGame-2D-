using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace ProjectAssets.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private PlayerView _playerView;
        private EnemyHealthController _enemyHealthController;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Inject]
        public void Construct(PlayerView playerView)
        {
            _playerView = playerView;
        }
        
        public void Initialize(float health, EnemyHealthController enemyHealthController)
        {
            _enemyHealthController = enemyHealthController;
            _enemyHealthController.SetHealth(health);
        }

        private void Start()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Update()
        {
            _agent.SetDestination(_playerView.transform.position);
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