using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class MeleeAttackState : StateEnemy
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        private Vector3 _initialPosition;
        private float _attackCooldown = 1.5f;
        private float _lastAttackTime;
        private float _attackDistance = 2.0f;

        public MeleeAttackState(StateMachine stateMachine, NavMeshAgent agent, Transform target) : base(stateMachine)
        {
            _agent = agent;
            _target = target;
        }

        public override void Update()
        {
            float distanceToTarget = Vector3.Distance(_agent.transform.position, _target.position);

            if (distanceToTarget > _attackDistance)
            {
                _stateMachine.Transit<ChaseState>();
                return;
            }

            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                PerformAttack();
                _lastAttackTime = Time.time;
            }
        }

        private void PerformAttack()
        {
            _initialPosition = _agent.transform.position;
            
            _agent.enabled = false;
            
            _agent.transform.position = _target.position;
            
            _agent.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(ReturnToInitialPositionAfterDelay(0.5f));
        }

        private System.Collections.IEnumerator ReturnToInitialPositionAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            _agent.transform.position = _initialPosition;
            
            _agent.enabled = true;
        }
    }
}
