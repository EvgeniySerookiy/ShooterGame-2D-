using System;

namespace UnityEngine.Advertisements
{
    public class Purchasers : MonoBehaviour
    {
        public event Action OnPurchaseComplete;
        public bool IsAdsPurchased()
        {
            return PlayerPrefs.GetInt("remove_ads") == 1;
        }
        private void Awake()
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