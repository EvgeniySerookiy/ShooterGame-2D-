using System.Collections;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class AttackState : StateEnemy
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        private readonly EnemySetting _enemySetting;
        private readonly Animator _animator;
        private readonly Collider2D _collider;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly HealthController _healthController;
       
        private Vector3 _originalPosition;
        private Vector3 _targetPosition;
        
        private float _lastAttackTime;
        private bool _isAttacking;
        

        public AttackState(StateMachine stateMachine, NavMeshAgent agent, Transform target, 
            EnemySetting enemySetting, Animator animator, Collider2D collider, MonoBehaviour monoBehaviour, 
            HealthController healthController) : base(stateMachine)
        {
            _agent = agent;
            _target = target;
            _enemySetting = enemySetting;
            _animator = animator;
            _collider = collider;
            _monoBehaviour = monoBehaviour;
            _healthController = healthController;
        }

        public override void Enter()
        {
            _animator.SetBool("Run", false);
        }

        private IEnumerator Attack()
        {
            _originalPosition = _agent.transform.position;
            _targetPosition = _target.position;

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
            
            if (!_isAttacking && Time.time >= _lastAttackTime + _enemySetting.AttackCooldown)
            {
                _monoBehaviour.StartCoroutine(Attack());
                _lastAttackTime = Time.time;
            }
        }
    }
}