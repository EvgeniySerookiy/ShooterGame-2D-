using System.Collections.Generic;
using ProjectAssets.Scripts.Buffs;
using ProjectAssets.Scripts.Buffs.Settings;
using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Enemy;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using ProjectAssets.Scripts.Weapon;
using ProjectAssets.Scripts.Weapon.Settings;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.ZenjectInstallers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _spawnEnemyPositions;
        [SerializeField] private Transform[] _gameBoard;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private Transform _buffRoot;
        [SerializeField] private WeaponListSettings weaponListSettings;
        [SerializeField] private EnemyListSettings enemyListSettings;
        [SerializeField] private BuffListSettings buffListSettings;
        [SerializeField] private Bullet _bulletPrefab;

        public override void InstallBindings()
        {
            Container.Bind<EnemyFactory>().AsSingle();
            
            Container.Bind<WeaponProvider>().AsSingle().WithArguments(weaponListSettings);
            Container.Bind<EnemyProvider>().AsSingle().WithArguments(enemyListSettings);
            Container.Bind<BuffProvider>().AsSingle().WithArguments(buffListSettings);
            Container.Bind<BuffFactory>().AsSingle().WithArguments(_buffRoot);
            Container.Bind<WeaponFactory>().AsSingle().WithArguments(_weaponRoot);
            Container.Bind<EnemySpawner>().AsSingle().WithArguments(_spawnEnemyPositions);
            
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameStageController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CoroutineLauncher>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerWeaponController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuffSpawner>().AsSingle().WithArguments(_gameBoard, buffListSettings);
            
            Container.Bind<Dictionary<WeaponType, WeaponController>>()
                .FromMethod(context => context.Container.Resolve<WeaponFactory>().Create())
                .AsSingle();
            
            Container.Bind<MonoBehaviour>().FromInstance(this).AsSingle();
            Container.Bind<Bullet>().FromInstance(_bulletPrefab).AsSingle();
            
        }
    }
}