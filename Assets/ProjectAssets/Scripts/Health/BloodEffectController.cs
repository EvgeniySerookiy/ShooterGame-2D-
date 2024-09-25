using ProjectAssets.Scripts.Blood;
using UnityEngine;

namespace ProjectAssets.Scripts.Health
{
    public class BloodEffectController : MonoBehaviour
    {
        [SerializeField] private float _bloodEffectLifetime = 0.3f;
        
        [SerializeField] private BloodEffectParticle _bloodEffectPrefab;
        

        public void InjectBloodEffectParticle(BloodEffectParticle bloodEffectPrefab)
        {
            _bloodEffectPrefab = bloodEffectPrefab;
        }
        
        public void ShowBloodEffect()
        {
            var bloodEffectObject = Instantiate(_bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(bloodEffectObject.gameObject, _bloodEffectLifetime);
        }
        
    }
}