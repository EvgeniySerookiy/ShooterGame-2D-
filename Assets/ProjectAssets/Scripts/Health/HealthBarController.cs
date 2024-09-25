using UnityEngine;
using UnityEngine.UI;

namespace ProjectAssets.Scripts.Health
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] private Image _healthBarImage;

        public void UpdateHealthBar(float healthPercentage)
        {
            _healthBarImage.fillAmount = healthPercentage;
        }
    }
}