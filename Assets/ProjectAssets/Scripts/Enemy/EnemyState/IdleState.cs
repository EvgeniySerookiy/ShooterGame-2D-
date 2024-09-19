using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using UnityEngine;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class IdleState : StateEnemy
    {
        private readonly Animator _animator;
        
        public IdleState(StateMachine stateMachine, Animator animator) : base(stateMachine)
        {
            _animator = animator;
        }
        
        public override void Enter()
        {
            _animator.SetBool("Run", false);
        }
    }
}