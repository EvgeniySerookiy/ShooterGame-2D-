using System.Collections;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.Health;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class AttackState : State
    {
        private readonly NavMeshAgent _agent;
        private readonly Player _player;
        private readonly EnemySetting _enemySetting;
        private readonly Animator _animator;
        private readonly Collider2D _collider;
        private readonly CoroutineLauncher _coroutineLauncher;
        private readonly HealthController _healthController;
       
        private Vector3 _originalPosition;
        private Vector3 _targetPosition;
        
        private float _lastAttackTime;
        private bool _isAttacking;
        

        public AttackState(StateMachine stateMachine, NavMeshAgent agent, Player player, 
            EnemySetting enemySetting, Animator animator, Collider2D collider, CoroutineLauncher coroutineLauncher, 
            HealthController healthController) : base(stateMachine)
        {
            _agent = agent;
            _player = player;
            _enemySetting = enemySetting;
            _animator = animator;
            _collider = collider;
            _coroutineLauncher = coroutineLauncher;
            _healthController = healthController;
        }

        public override void Enter()
        {
            _animator.SetBool("Run", false);
        }

        private IEnumerator Attack()
        {
            _originalPosition = _agent.transform.position;
            _targetPosition = _player.transform.position;

            _agent.enabled = false;
            _collider.isTrigger = true;
            _isAttacking = true;

            float percent = 0f;
            while (percent <= 1)
            {
                percent += Time.deltaTime * _enemySetting.AttackSpeed;
                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
                _agent.transform.position = Vector2.Lerp(_originalPosition, _targetPosition, interpolation);
                yield return null;
            }
            
            _agent.transform.position = _originalPosition;
            _agent.enabled = true;
            _collider.isTrigger = false;
            _isAttacking = false;
            
            _stateMachine.Transit<ChaseState>();
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
            
            if (!_isAttacking && Time.time >= _lastAttackTime + _enemySetting.AttackCooldown)
            {
                _coroutineLauncher.StartCoroutine(Attack());
                _lastAttackTime = Time.time;
            }
        }
    }
}