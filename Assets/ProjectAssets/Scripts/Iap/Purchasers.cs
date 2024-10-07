using System;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Iap
{
    public class Purchasers : IPurchasers, IInitializable
    {
        public event Action OnPurchaseComplete;
        
        public bool IsAdsPurchased()
        {
            return PlayerPrefs.GetInt("remove_ads") == 1;
        }
        
        public void Initialize()
        {
            PlayerPrefs.SetInt("remove_ads", 0);
        }

        public void CompletePurchase()
        {
            OnPurchaseComplete?.Invoke();
            RemoveAds();
        }

        private void RemoveAds()
        {
            Debug.Log("RemoveAds");
            PlayerPrefs.SetInt("remove_ads", 1);
        }
    }
}