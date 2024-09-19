using UnityEngine.SceneManagement;

namespace ProjectAssets.Scripts.Scenes
{
    public class GameSceneManager
    {
        public void LoadGameScene()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
        }
    }
}