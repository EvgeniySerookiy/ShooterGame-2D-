using ProjectAssets.Scripts.Scenes;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSceneManager>().AsSingle();
        }
    }
}