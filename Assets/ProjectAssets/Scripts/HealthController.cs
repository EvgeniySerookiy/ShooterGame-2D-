using ProjectAssets.Scripts.Blood;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class HealthController : MonoBehaviour
    {
        public float Health { get; private set; }
        [SerializeField] private BloodEffectParticle _bloodEffectPrefab;
        [SerializeField] private Image _healthBarHealth;
        
        public bool _isDead;
        private float _maxHealth;
        private GameStageController _gameStageController;

        [Inject]
        public void Construct(GameStageController gameStageController)
        {
            _gameStageController = gameStageController;
        }
        
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
            if (_isDead) return;

            var bloodEffectObject = Instantiate(_bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(bloodEffectObject.gameObject, 0.5f);

            Health -= damage;

            if (_healthBarHealth != null)
            {
                _healthBarHealth.fillAmount = Health / _maxHealth;
            }

            if (Health <= 0)
            {
                Health = 0;
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            
            _gameStageController.OnEnemyKilled();
            Destroy(gameObject, 2f);
        }
    }
}