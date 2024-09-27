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
            _healthController.HealthUpdated += UpdateHealthBar;
        }

        private void UpdateHealthBar(float healthPercentage)
        {
            _healthBarImage.fillAmount = healthPercentage;
        }

        private void OnDestroy()
        {
            _healthController.HealthUpdated -= UpdateHealthBar;
        }
    }
}