using ProjectAssets.Scripts.Blood;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectAssets.Scripts
{
    public class HealthController : MonoBehaviour
    {
        public float Health { get; private set; }
        [SerializeField] private Image _healthBarHealth;
        [SerializeField] private BloodEffectParticle _bloodEffectPrefab;
        [SerializeField] private float _bloodEffectLifetime;
        
        private float _maxHealth;
        
        public void SetHealth(float health)
        {
            Health = health;
            _maxHealth = Health;
        }
        
        public void SetHieEffectPrefab(BloodEffectParticle bloodEffectPrefab)
        {
            _bloodEffectPrefab = bloodEffectPrefab;
        }
        
        public void TakeDamage(float damage)
        {
            var bloodEffectObject = Instantiate(_bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(bloodEffectObject.gameObject, _bloodEffectLifetime);
            
            Health -= damage;

            if (_healthBarHealth != null)
            {
                _healthBarHealth.fillAmount = Health / _maxHealth;
            }

            if (Health <= 0)
            {
                Health = 0;
            }
        }
    }
}