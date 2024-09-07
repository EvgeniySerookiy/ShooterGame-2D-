using System.Collections;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class FiringState : StateEnemy
    {
        private readonly Bullet _bulletPrefab;
        private readonly EnemySetting _enemySetting;
        private readonly Animator _animator;
        private readonly Transform _muzzle;
        private readonly PlayerView _playerView;
        private readonly HealthController _healthController;
        private readonly MonoBehaviour _monoBehaviour;
        
        private BulletPoolManager _bulletPoolManager;
        private float _lastAttackTime;
        private bool _isAttacking;
        private bool _isEnemyShooting = true;
        private bool _canPenetrate;
        

        public FiringState(StateMachine stateMachine, Bullet bulletPrefab, Transform muzzle, 
            PlayerView playerView, EnemySetting enemySetting, Animator animator, HealthController healthController,
            MonoBehaviour monoBehaviour) 
            : base(stateMachine)
        {
            _healthController = healthController;
            _bulletPrefab = bulletPrefab;
            _monoBehaviour = monoBehaviour;
            _animator = animator;
            _muzzle = muzzle;
            _enemySetting = enemySetting;
            _playerView = playerView;
            _bulletPoolManager = new BulletPoolManager(_muzzle, _bulletPrefab, 
                _enemySetting.AttackSpeed, _isEnemyShooting, _canPenetrate);
        }
        
        
        public override void Enter()
        {
            _animator.SetBool("Run", false);
        }
        
        private IEnumerator Fire()
        {
            _isAttacking = true;
            var bullet = _bulletPoolManager._bulletPool.Get();
            bullet.Shoot(_playerView.transform.position, null, _enemySetting.Damage);

            yield return new WaitForSeconds(_enemySetting.AttackCooldown);

            _isAttacking = false;
            
            _stateMachine.Transit<ChaseState>();
        }

        public override void Update()
        {
            if (_healthController.Health == 0)
            {
                _stateMachine.Transit<DeathState>();
                return;
            }
            
            float distanceToPlayer = Vector2.Distance(_playerView.transform.position, _muzzle.position);

            if (distanceToPlayer > _enemySetting.AttackDistance)
            {
                _stateMachine.Transit<ChaseState>();
                return;
            }

            if (!_isAttacking && Time.time >= _lastAttackTime + _enemySetting.AttackCooldown)
            {
                _monoBehaviour.StartCoroutine(Fire());
                _lastAttackTime = Time.time;
            }
        }
    }
}
