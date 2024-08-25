//using ProjectAssets.Scripts.PlayerStateMachine;
//using UnityEngine;
//
//namespace ProjectAssets.Scripts.PlayerState
//{
//    public class MoveState : StatePlayer
//    {
//        private readonly Rigidbody2D _rigidbody;
//        private readonly float _speed;
//        private Vector2 _movementDirection;
//
//        public MoveState(StateMachine stateMachine, Rigidbody2D rigidbody, float speed) : base(stateMachine)
//        {
//            _rigidbody = rigidbody;
//            _speed = speed;
//        }
//
//        public void SetMovementDirection(Vector2 direction)
//        {
//            _movementDirection = direction;
//        }
//        
//
//        public override void Update()
//        {
//            Debug.Log("Сейчас MoveState");
//            SetMovement(_movementDirection);
//        }
//
//        private void SetMovement(Vector2 direction)
//        {
//            _rigidbody.velocity = direction.normalized * _speed;
//        }
//    }
//}