using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace ProjectAssets.Scripts.Ads
{
    public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private bool _adsEnabled = true;
        private const string REWARDED_VIDEO = "Rewarded_Android";
        
        public event Action OnAdCompleted;

        public void DisableAds()
        {
            _adsEnabled = false;
        }
        
        public void Load()
        {
            Advertisement.Load(REWARDED_VIDEO, this);
        }

        public void Show()
        {
            if (_adsEnabled)
            {
                Advertisement.Show(REWARDED_VIDEO, this);
            }
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"Ad Loaded: {placementId}");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"Failed to load Ad {placementId}: {error} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Failed to show Ad {placementId}: {error} - {message}");
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
            if (placementId.Equals(REWARDED_VIDEO) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                OnAdCompleted?.Invoke();
            }

            Load(); 
        }
    }
}
