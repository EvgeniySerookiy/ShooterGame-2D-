using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.Health;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class ChaseState : State
    {
        private readonly NavMeshAgent _agent;
        private readonly Player _player;
        private readonly EnemySetting _enemySetting;
        private readonly Animator _animator;
        private readonly HealthController _healthController;

        public ChaseState(StateMachine stateMachine, Player player, EnemySetting enemySetting, 
            Animator animator, NavMeshAgent agent, HealthController healthController) : base(stateMachine)
        {
            _agent = agent;
            _player = player;
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
            if (_player.IsDead)
            {
                _stateMachine.Transit<IdleState>();
                return;
            }
            
            if (_healthController.CurrentHealth == 0)
            {
                _stateMachine.Transit<DeathState>();
                return;
            }
            _agent.SetDestination(_player.transform.position);
            
            float distanceToTarget = Vector2.Distance(_agent.transform.position, _player.transform.position);
            
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