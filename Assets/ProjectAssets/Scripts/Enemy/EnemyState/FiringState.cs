using System.Collections;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Enemy.EnemyStateMachine;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace ProjectAssets.Scripts.Enemy.EnemyState
{
    public class FiringState : StateEnemy
    {
        private readonly BulletEnemy _bulletEnemyPrefab;
        private readonly ObjectPool<BulletEnemy> _bulletPool;
        private readonly EnemySetting _enemySetting;
        private readonly Animator _animator;
        private readonly Transform _muzzle;
        private readonly PlayerView _playerView;
        private readonly HealthController _healthController;
        private readonly MonoBehaviour _monoBehaviour;
        
        private float _lastAttackTime;
        private bool _isAttacking;

        public FiringState(StateMachine stateMachine, BulletEnemy bulletEnemyPrefab, Transform muzzle, 
            PlayerView playerView, EnemySetting enemySetting, Animator animator, HealthController healthController,
            MonoBehaviour monoBehaviour, NavMeshAgent agent) 
            : base(stateMachine)
        {
            _healthController = healthController;
            _bulletEnemyPrefab = bulletEnemyPrefab;
            _monoBehaviour = monoBehaviour;
            _animator = animator;
            _muzzle = muzzle;
            _enemySetting = enemySetting;
            _playerView = playerView;
            _bulletPool = new ObjectPool<BulletEnemy>(CreateBullet, OnGetBullet, OnReleaseBullet, defaultCapacity: 100);
        }

        private BulletEnemy CreateBullet()
        {
            var bullet = Object.Instantiate(_bulletEnemyPrefab, _muzzle);
            bullet.Initialize(_enemySetting);
            return bullet;
        }

        private void OnGetBullet(BulletEnemy bullet)
        {
            bullet.Hitted += _bulletPool.Release;
            bullet.transform.parent = null;
            bullet.transform.position = _muzzle.position;
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(BulletEnemy bullet)
        {
            bullet.Hitted -= _bulletPool.Release;
            bullet.transform.parent = _muzzle;
            bullet.gameObject.SetActive(false);
        }
        
        public override void Enter()
        {
            _animator.SetBool("Run", false);
        }
        
        private IEnumerator Fire()
        {
            _isAttacking = true;
            var bullet = _bulletPool.Get();  // Получаем пулю из пула
            bullet.Shoot(_playerView.transform.position, _enemySetting.Damage);  // Запускаем пулю в направлении цели
            
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
            
            if (!_isAttacking && Time.time >= _lastAttackTime + _enemySetting.AttackCooldown)
            {
                _monoBehaviour.StartCoroutine(Fire());
                _lastAttackTime = Time.time;
            }
        }
    }
}
