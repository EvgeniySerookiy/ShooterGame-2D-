using ProjectAssets.Scripts.Enemy.EnemyState;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy
{
    public class NavMeshEnemy : EnemyBase
    {
        [SerializeField] private NavMeshAgent _agent;

        public override void SetupStates()
        {
            Debug.Log("SetupStates");
            if (_agent != null)
            {
                _agent.updateRotation = false;
                _agent.updateUpAxis = false;
            }
            var chaseState = new ChaseState(_stateMachine, _playerView.transform, _enemySetting, _animator, _enemy, _agent);
            var damnMeleeAttackState = new DamnMeleeAttackState(_stateMachine, _agent, _playerView.transform, _enemySetting, _animator, _collider, _monoBehaviour);
            _stateMachine.AddStates(chaseState, damnMeleeAttackState);
        }
    }
}