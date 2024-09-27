using ProjectAssets.Scripts.Blood;
using UnityEngine;

namespace ProjectAssets.Scripts.Health
{
    public class BloodEffectController : MonoBehaviour
    {
        [SerializeField] private float _bloodEffectLifetime = 0.3f;

        [SerializeField] private HealthController _healthController;
        [SerializeField] private BloodEffectParticle _bloodEffectPrefab;

        public void InjectHealthController(HealthController healthController, BloodEffectParticle bloodEffectPrefab)
        {
            _healthController = healthController;
            _bloodEffectPrefab = bloodEffectPrefab;
        }
        
        private void Start()
        {
            _healthController.OnBloodEffect += ShowBloodEffect;
        }

        private void ShowBloodEffect()
        {
            var bloodEffectObject = Instantiate(_bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(bloodEffectObject.gameObject, _bloodEffectLifetime);
        }

        private void OnDestroy()
        {
            _healthController.OnBloodEffect -= ShowBloodEffect;
        }
    }
}