using UnityEngine;
using UnityEngine.UI;

namespace ProjectAssets.Scripts.Health
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private HealthController _healthController;
        [SerializeField] private Image _healthBarImage;

        private void Awake()
        {
            _healthController.OnHealthChanged += UpdateHealthBar;
        }
        
        private void OnDestroy()
        {
            _healthController.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            _healthBarImage.fillAmount = _healthController.CurrentHealth / _healthController.MaxHealth;
        }

        
    }
}