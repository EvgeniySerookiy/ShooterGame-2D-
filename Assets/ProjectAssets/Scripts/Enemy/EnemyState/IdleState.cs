using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class IdleState : StateEnemy
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