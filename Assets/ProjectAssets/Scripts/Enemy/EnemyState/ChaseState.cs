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
        private readonly HealthController _healthController;

        public ChaseState(StateMachine stateMachine, Transform target, EnemySetting enemySetting, 
            Animator animator, NavMeshAgent agent, HealthController healthController) : base(stateMachine)
        {
            _agent = agent;
            _target = target;
            _enemySetting = enemySetting;
            _animator = animator;
            _agent.speed = _enemySetting.Speed;
            _agent.stoppingDistance = _enemySetting.AttackDistance;
            _healthController = healthController;
        }

        public override void Enter()
        {
            _animator.SetBool("Run",true);
        }

        public override void Update()
        {
            //if (_target.gameObject == null)
            //{
            //    _stateMachine.Transit<IdleState>();
            //    return;
            //}
            
            if (_healthController.Health == 0)
            {
                _stateMachine.Transit<DeathState>();
                return;
            }
            _agent.SetDestination(_target.position);
            
            float distanceToTarget = Vector2.Distance(_agent.transform.position, _target.position);
            
            if (distanceToTarget <= _agent.stoppingDistance)
            {
                if (_enemySetting.Type == EnemyType.Raging)
                {
                    _stateMachine.Transit<FiringState>();
                }
                else
                {
                    _stateMachine.Transit<AttackState>();
                }
            }
        }
    }
}