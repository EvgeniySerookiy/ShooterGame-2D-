using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Health
{
    public class HealthController : MonoBehaviour
    { 
        public event Action OnHealthChanged;
        public float Health { get; private set; }
        private float _maxHealth;
        
        [SerializeField] private HealthBarController _healthBarController;
        [SerializeField] private BloodEffectController _bloodEffectController;

        public void InjectBloodEffectController(BloodEffectController bloodEffectController)
        {
            _bloodEffectController = bloodEffectController;
        }
        
        public void SetHealth(float health)
        {
            Health = health;
            _maxHealth = health;
        }

        public void TakeDamage(float damage)
        {
            Health = Mathf.Max(Health - damage, 0);
            OnHealthChanged?.Invoke();
            _bloodEffectController.ShowBloodEffect();
            _healthBarController?.UpdateHealthBar(Health / _maxHealth);
        }
    }
}