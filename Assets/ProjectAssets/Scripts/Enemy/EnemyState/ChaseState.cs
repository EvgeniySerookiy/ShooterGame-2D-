using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class ChaseState : StateEnemy
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        private readonly float _attackDistance;

        public ChaseState(StateMachine stateMachine, NavMeshAgent agent, Transform target) : base(stateMachine)
        {
            _agent = agent;
            _target = target;
            _attackDistance = _agent.stoppingDistance;
        }

        public override void Update()
        {
            _agent.SetDestination(_target.position);
            
            float distanceToTarget = Vector3.Distance(_agent.transform.position, _target.position);
            
            if (distanceToTarget <= _attackDistance)
            {
                _stateMachine.Transit<MeleeAttackState>();
            }
        }
    }
}