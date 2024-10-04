using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Health
{
    public class HealthController : MonoBehaviour
    { 
        public event Action OnHealthChanged;
        public event Action OnBloodEffect;
        public event Action<float> HealthUpdated;
        
        public float Health { get; private set; }
        private float _maxHealth;
        
        public void SetHealth(float health)
        {
            Health = health;
            _maxHealth = health;
            HealthUpdated?.Invoke(Health / _maxHealth);
        }

        public void TakeDamage(float damage)
        {
            Health = Mathf.Max(Health - damage, 0);
            OnHealthChanged?.Invoke();
            OnBloodEffect?.Invoke();
            HealthUpdated?.Invoke(Health / _maxHealth);
        }
    }
}