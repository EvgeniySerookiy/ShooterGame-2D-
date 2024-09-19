using ProjectAssets.Scripts.Scenes;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class StartSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StartScene>().FromComponentInHierarchy().AsSingle();
        }
    }
}