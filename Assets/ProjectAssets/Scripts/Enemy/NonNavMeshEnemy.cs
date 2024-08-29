using ProjectAssets.Scripts.Enemy.EnemyState;

namespace ProjectAssets.Scripts.Enemy
{
    public class NonNavMeshEnemy : EnemyBase
    {
        public override void SetupStates()
        {
            var chaseState = new ChaseState(_stateMachine, _playerView.transform, _enemySetting, _animator, _enemy, null);
            var damnMeleeAttackState = new DamnMeleeAttackState(_stateMachine, null, _playerView.transform, _enemySetting, _animator, _collider, _monoBehaviour);
            _stateMachine.AddStates(chaseState, damnMeleeAttackState);
        }
    }
}