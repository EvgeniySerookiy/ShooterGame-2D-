using ProjectAssets.Scripts.Cursor;
using ProjectAssets.Scripts.Cursor.Settings;
using ProjectAssets.Scripts.Scenes;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.ZenjectInstallers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CursorSetting _cursorSetting;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSceneManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CursorChanger>().AsSingle().WithArguments(_cursorSetting);
        }
    }
}