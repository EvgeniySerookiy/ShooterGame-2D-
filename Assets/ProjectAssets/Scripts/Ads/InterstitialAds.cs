using UnityEngine;
using UnityEngine.Advertisements;

namespace ProjectAssets.Scripts.Ads
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private string _adID;
        private bool _adsEnabled = true;
        
        [SerializeField] private string _androidAdID = "Interstitial_Android";
        

        public void DisableAds()
        {
            _adsEnabled = false;
        }
        
        private void Awake()
        {
            _adID = _androidAdID;
            LoadAd();
        }

        public void LoadAd()
        {
            Advertisement.Load(_adID, this);
        }
        
        public void Show()
        {
            if (_adsEnabled)
            {
                Advertisement.Show(_adID, this);
            }
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"Ad Loaded: {placementId}");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"Failed to load Ad {placementId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Failed to show Ad {placementId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"Ad Started: {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"Ad Clicked: {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            LoadAd();
        }
    }
}