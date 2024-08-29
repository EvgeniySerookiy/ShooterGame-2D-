using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class ChaseState : StateEnemy
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        private readonly EnemySetting _enemySetting;
        private readonly Animator _animator;

        public ChaseState(StateMachine stateMachine, NavMeshAgent agent, Transform target, EnemySetting enemySetting, Animator animator) : base(stateMachine)
        {
            _agent = agent;
            _target = target;
            _enemySetting = enemySetting;
            _animator = animator;

            _agent.speed = _enemySetting.Speed;
            _agent.stoppingDistance = _enemySetting.AttackDistance;
        }

        public override void Enter()
        {
            _animator.SetBool("Run",true);
        }

        public override void Update()
        {
            _agent.SetDestination(_target.position);
            
            float distanceToTarget = Vector2.Distance(_agent.transform.position, _target.position);
            
            if (distanceToTarget <= _agent.stoppingDistance)
            {
                if(_enemySetting.Type == EnemyType.Damn) 
                    _stateMachine.Transit<DamnMeleeAttackState>();
            }
        }
    }
}