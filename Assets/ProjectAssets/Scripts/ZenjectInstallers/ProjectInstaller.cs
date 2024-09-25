using ProjectAssets.Scripts.Scenes;
using Zenject;

namespace ProjectAssets.Scripts.ZenjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameSceneManager>().AsSingle();
        }
    }
}