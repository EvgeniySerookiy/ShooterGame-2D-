//using System;
//using System.Collections.Generic;
//
//namespace ProjectAssets.Scripts.PlayerStateMachine
//{
//    public class StateMachine
//    {
//        private Dictionary<Type, StatePlayer> _states = new();
//        private StatePlayer _currentState;
//
//        public void AddStates(params StatePlayer[] states)
//        {
//            foreach (var state in states)
//            {
//                var type = state.GetType();
//                _states.TryAdd(type, state);
//            }
//        }
//
//        public void Transit<T>() where T : StatePlayer
//        {
//            var type = typeof(T);
//            if (_states.TryGetValue(type, out var state))
//            {
//                _currentState?.Exit();
//                _currentState = state;
//                _currentState.Enter();
//            }
//            else
//            {
//                throw new InvalidOperationException($"State {type} not found in state machine");
//            }
//        }
//
//        public void Update()
//        {
//            _currentState?.Update();
//        }
//    }
//}