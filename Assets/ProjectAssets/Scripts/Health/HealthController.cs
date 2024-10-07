using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Health
{
    public class HealthController : MonoBehaviour
    { 
        public event Action OnHealthChanged;
        
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        
        public void SetHealth(float health)
        {
            CurrentHealth = health;
            MaxHealth = health;
            OnHealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            OnHealthChanged?.Invoke();
        }
    }
}