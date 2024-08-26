namespace ProjectAssets.Scripts.Enemy.EnemyStateMachine
{
    public abstract class StateEnemy
    {
        protected readonly StateMachine _stateMachine;

        protected StateEnemy(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}