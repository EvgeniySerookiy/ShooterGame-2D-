using ProjectAssets.Scripts.PlayerCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectAssets.Scripts.Scenes
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private Button _buttonRestart;
        
        private GameSceneManager _gameSceneManager;
        private Player _player;
        
        [Inject]
        public void Construct(GameSceneManager gameSceneManager, Player player)
        {
            _gameSceneManager = gameSceneManager;
            _player = player;
            _player.OnDie += GameOver;
            _buttonRestart.onClick.AddListener(RestartGame);
            _gameOverText.gameObject.SetActive(false);
            _buttonRestart.gameObject.SetActive(false);
        }

        public void GameOver()
        {
            _gameOverText.gameObject.SetActive(true);
            _waveText.gameObject.SetActive(false);
            _buttonRestart.gameObject.SetActive(true);
        }
        
        public void RestartGame()
        {
            _player.OnDie -= GameOver;
            _gameSceneManager.LoadGameScene();
            _buttonRestart.onClick.RemoveListener(RestartGame);
        }
    }
}