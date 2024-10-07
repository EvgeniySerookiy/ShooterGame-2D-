using System;

namespace ProjectAssets.Scripts.Ads
{
    public interface IRewardedAds
    {
        event Action OnAdCompleted;
        void Show();
        void Load();
        void DisableAds();
    }
}