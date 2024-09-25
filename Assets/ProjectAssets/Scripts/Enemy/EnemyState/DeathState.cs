using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;
using StateMachine = ProjectAssets.Scripts.Enemy.EnemyStateMachine.StateMachine;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class DeathState : State
    {
        private const string DEATH_ANIMATION = "DeathAnimation";
        private const string DEATH_LAYER = "Death";
        
        private readonly Animator _animator;
        private readonly NavMeshAgent _agent;
        private readonly SpriteRenderer _spriteRenderer;
        public DeathState(StateMachine stateMachine, Animator animator, NavMeshAgent agent, 
            SpriteRenderer spriteRenderer) : base(stateMachine)
        {
            _agent = agent;
            _animator = animator;
            _spriteRenderer = spriteRenderer;
        }
        
        public override void Enter()
        {
            _animator.Play(DEATH_ANIMATION);
            _spriteRenderer.sortingLayerName = DEATH_LAYER;
            _agent.enabled = false;
        }
    }
}