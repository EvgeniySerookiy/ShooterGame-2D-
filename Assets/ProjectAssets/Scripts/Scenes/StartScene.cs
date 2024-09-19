using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProjectAssets.Scripts.Scenes
{
    public class StartScene : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;

        private GameSceneManager _gameSceneManager;
        
        [Inject]
        public void Construct(GameSceneManager gameSceneManager)
        {
            _gameSceneManager = gameSceneManager;
        }

        private void Awake()
        {
            _buttonStart.onClick.AddListener(_gameSceneManager.LoadGameScene);
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(_gameSceneManager.LoadGameScene);
        }
    }
}