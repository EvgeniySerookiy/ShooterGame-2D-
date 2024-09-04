using ProjectAssets.Scripts.Blood;
using UnityEngine;

namespace ProjectAssets.Scripts
{
    public class HealthController : MonoBehaviour
    {
        public float Health { get; private set; }
        [SerializeField] private BloodEffectParticle _bloodEffectPrefab;

        public void SetHealth(float health)
        {
            Health = health;
        }
        
        public void SetHieEffectPrefab(BloodEffectParticle bloodEffectPrefab)
        {
            _bloodEffectPrefab = bloodEffectPrefab;
        }
        
        public void TakeDamage(float damage)
        {
            var bloodEffectObject = Instantiate(_bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(bloodEffectObject, 0.5f);
            
            Health -= damage;
            
            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject, 2f);
        }
    }
}