using Zenject;
using System;
using UnityEngine;

namespace ProjectAssets.Scripts
{
    public class GameInput : ITickable
    {
        public event Action<Vector2> OnMove;
        public event Action OnShoot;

        public void Tick()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                Vector2 direction = new Vector2(horizontal, vertical);
                OnMove?.Invoke(direction);
            }

            if (horizontal == 0 && vertical == 0)
            {
                Vector2 direction = Vector2.zero;
                OnMove?.Invoke(direction);
            }
            
            OnShoot?.Invoke();
        }
    }
}