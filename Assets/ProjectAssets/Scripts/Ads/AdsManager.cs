using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

namespace ProjectAssets.Scripts.Ads
{
    public class AdsManager : IUnityAdsInitializationListener, IInitializable
    {
        private const string GAME_ID = "5706870";

        private bool _testMode = true;

        public void Initialize()
        {
            Advertisement.Initialize(GAME_ID, _testMode, this);
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
