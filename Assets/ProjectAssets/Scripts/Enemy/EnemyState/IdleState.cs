using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    // Доработать
    public class IdleState : State
    {
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        
        public IdleState(StateMachine stateMachine, Animator animator, NavMeshAgent agent) : base(stateMachine)
        {
            _animator = animator;
            _agent = agent;
        }
        
        public override void Enter()
        {
            _animator.SetBool("Run", false);
        }
    }
}