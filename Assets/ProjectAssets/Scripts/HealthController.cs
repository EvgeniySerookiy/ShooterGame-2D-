using UnityEngine;

namespace ProjectAssets.Scripts
{
    public class HealthController : MonoBehaviour
    {
        public float _health;

        public void SetHealth(float health)
        {
            _health = health;
        }
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}