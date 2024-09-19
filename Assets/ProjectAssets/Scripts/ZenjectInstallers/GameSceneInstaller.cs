using ProjectAssets.Scripts.Buffs;
using ProjectAssets.Scripts.Buffs.Settings;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Cursor;
using ProjectAssets.Scripts.Cursor.Settings;
using ProjectAssets.Scripts.Enemy;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using ProjectAssets.Scripts.Root;
using ProjectAssets.Scripts.Weapon;
using ProjectAssets.Scripts.Weapon.Settings;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _spawnEnemyPositions;
        [SerializeField] private WeaponSettings _weaponSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private BuffSettings _buffSettings;
        [SerializeField] private CursorSetting _cursorSetting;
        [SerializeField] private Bullet _bulletPrefab;

        public override void InstallBindings()
        {
            Container.Bind<WeaponProvider>().AsSingle().WithArguments(_weaponSettings);
            Container.Bind<EnemyProvider>().AsSingle().WithArguments(_enemySettings);
            Container.Bind<BuffProvider>().AsSingle().WithArguments(_buffSettings);
            
            Container.Bind<MultiRoot>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameStageController>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<WeaponFactory>().AsSingle();
            Container.Bind<BuffFactory>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<PlayerWeaponController>().AsSingle();

            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemySpawner>().AsSingle().WithArguments(_spawnEnemyPositions);
            Container.Bind<BuffSpawner>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CursorChanger>().AsSingle().WithArguments(_cursorSetting);
            
            Container.Bind<MonoBehaviour>().FromInstance(this).AsSingle();
            
            Container.Bind<Bullet>().FromInstance(_bulletPrefab).AsSingle();
            
        }
    }
}