using System;
using System.Collections.Generic;

namespace ProjectAssets.Scripts.Enemy.EnemyStateMachine
{
    public class StateMachine
    {
        private Dictionary<Type, State> _states = new();
        private State _currentState;

        public void AddStates(params State[] states)
        {
            foreach (var state in states)
            {
                var type = state.GetType();
                _states.TryAdd(type, state);
            }
        }

        public void Transit<T>() where T : State
        {
            var type = typeof(T);
            if (_states.TryGetValue(type, out var state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                throw new InvalidOperationException($"State {type} not found in state machine");
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}