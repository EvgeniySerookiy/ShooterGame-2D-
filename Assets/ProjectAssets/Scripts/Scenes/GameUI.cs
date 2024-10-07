using System.Collections;
using ProjectAssets.Scripts.Ads;
using ProjectAssets.Scripts.Iap;
using ProjectAssets.Scripts.PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectAssets.Scripts.Scenes
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Button _buttonRemoveAds;

        private GameSceneManager _gameSceneManager;
        private IPurchasers _purchasers;
        private IInterstitialAds _interstitialAds;
        private IRewardedAds _rewardedAds;
        private Player _player;

        [Inject]
        public void Construct(GameSceneManager gameSceneManager, Player player, IPurchasers purchasers, 
            IInterstitialAds interstitialAds, IRewardedAds rewardedAds)
        {
            _rewardedAds = rewardedAds;
            _interstitialAds = interstitialAds;
            _purchasers = purchasers;
            _gameSceneManager = gameSceneManager;
            _rewardedAds.OnAdCompleted += ContinueGame;
            _player = player;
            _player.OnDying += Dying;
            _purchasers.OnPurchaseComplete += DisableAds;
            
            _buttonRemoveAds.onClick.AddListener(() => _purchasers.CompletePurchase());
            _buttonRestart.onClick.AddListener(RestartGame);
            _buttonPause.onClick.AddListener(PauseGame);
            _buttonPlay.onClick.AddListener(PlayGame);
            _buttonContinue.onClick.AddListener(ShowRewardedAd);

            SetInitialUIState();
        }
        
        private void SetInitialUIState()
        {
            _buttonRemoveAds.gameObject.SetActive(false);
            _gameOverText.gameObject.SetActive(false);
            _buttonRestart.gameObject.SetActive(false);
            _buttonPlay.gameObject.SetActive(false);
            _buttonContinue.gameObject.SetActive(false);
        }
        
        private void DisableAds()
        {
            _interstitialAds.DisableAds();
            _rewardedAds.DisableAds();
        }

        public void Dying()
        {
            _gameOverText.gameObject.SetActive(true);
            _waveText.gameObject.SetActive(false);
            _buttonRestart.gameObject.SetActive(true);
            _buttonPause.gameObject.SetActive(false);
            _buttonPlay.gameObject.SetActive(false);
            _buttonContinue.gameObject.SetActive(true);
            
            _rewardedAds.Load();
        }
        
        private void ContinueGame()
        {
            Debug.Log("ContinueGame");
            _player.Continue();
            _gameOverText.gameObject.SetActive(false);
            _waveText.gameObject.SetActive(false);
            _buttonRestart.gameObject.SetActive(false);
            _buttonPause.gameObject.SetActive(true);
            _buttonContinue.gameObject.SetActive(false);
        }
        

        private void ShowRewardedAd()
        {
            if (_purchasers.IsAdsPurchased())
            {
                ContinueGame();
            }
            else
            {
                _rewardedAds.Show();
            }
        }
        
        public void UpdateRemoveAdsButton()
        {
            bool removeAds = PlayerPrefs.GetInt("remove_ads") == 1;
            Debug.Log(removeAds);
            _buttonRemoveAds.gameObject.SetActive(!removeAds);
        }
        
        public void RestartGame()
        {
            _player.PlayDeathAnimation();
            StartCoroutine(RestartAfterDeath());
        }
        
        private IEnumerator RestartAfterDeath()
        {
            yield return new WaitForSeconds(_player._spriteLifetime);
            Time.timeScale = 1;
            _player.Died();
            _player.OnDying -= Dying;
            _buttonRestart.onClick.RemoveListener(RestartGame);
            _gameSceneManager.LoadGameScene();
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            UpdateRemoveAdsButton();
            _buttonPause.gameObject.SetActive(false);
            _buttonPlay.gameObject.SetActive(true);
            _interstitialAds.Show();
        }

        public void PlayGame()
        {
            Time.timeScale = 1;
            UpdateRemoveAdsButton();
            _buttonRemoveAds.gameObject.SetActive(false);
            _buttonPlay.gameObject.SetActive(false);
            _buttonPause.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _buttonRemoveAds.onClick.AddListener(() => _purchasers.CompletePurchase());
            _buttonPause.onClick.RemoveListener(PauseGame);
            _buttonPlay.onClick.RemoveListener(PlayGame);
            _buttonContinue.onClick.RemoveListener(ShowRewardedAd);
        }
    }
}
