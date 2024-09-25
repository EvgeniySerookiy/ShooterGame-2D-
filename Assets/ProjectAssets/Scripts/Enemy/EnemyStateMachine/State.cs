namespace ProjectAssets.Scripts.Enemy.EnemyStateMachine
{
    public abstract class State
    {
        protected readonly StateMachine _stateMachine;

        protected State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}