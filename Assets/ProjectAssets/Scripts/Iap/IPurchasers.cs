using System;

namespace ProjectAssets.Scripts.Iap
{
    public interface IPurchasers
    {
        event Action OnPurchaseComplete;

        bool IsAdsPurchased();
        void CompletePurchase();
    }
}