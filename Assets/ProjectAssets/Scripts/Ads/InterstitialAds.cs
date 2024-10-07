using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace ProjectAssets.Scripts.Ads
{
    public class InterstitialAds : IUnityAdsLoadListener, IUnityAdsShowListener, IInitializable, IInterstitialAds
    {
        private const string _ANDROID_ADID = "Interstitial_Android";
        
        private string _adID;
        private bool _adsEnabled = true;
        
        public void Initialize()
        {
            _adID = _ANDROID_ADID;
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
        
        public void DisableAds()
        {
            _adsEnabled = false;
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