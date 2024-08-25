//using ProjectAssets.Scripts.PlayerStateMachine;
//using UnityEngine;
//
//namespace ProjectAssets.Scripts.PlayerState
//{
//    public class IdleState : StatePlayer
//    {
//        private readonly Rigidbody2D _rigidbody;
//
//        public IdleState(StateMachine stateMachine, Rigidbody2D rigidbody) : base(stateMachine)
//        {
//            _rigidbody = rigidbody;
//        }
//        
//
//        public override void Update()
//        {
//            Debug.Log("Сейчас IdleState");
//            _rigidbody.velocity = Vector2.zero;
//        }
//    }
//}