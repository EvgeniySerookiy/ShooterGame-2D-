using UnityEngine;
using UnityEngine.Advertisements;

namespace ProjectAssets.Scripts.Ads
{
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string GAME_ID = "5706870";
        
        private bool _adsEnabled = true;

        [SerializeField] private bool _testMod = true;

        private void Awake()
        {
            Advertisement.Initialize(GAME_ID, _testMod, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads Initialization Complete");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Ads Initialization Failed");
        }
    }
}
