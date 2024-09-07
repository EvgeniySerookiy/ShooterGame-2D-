using ProjectAssets.Scripts.Bullets;
using ProjectAssets.Scripts.Cursor;
using ProjectAssets.Scripts.Cursor.Settings;
using ProjectAssets.Scripts.Enemy;
using ProjectAssets.Scripts.Enemy.Settings;
using ProjectAssets.Scripts.PlayerCharacter;
using ProjectAssets.Scripts.Weapon;
using ProjectAssets.Scripts.Weapon.Settings;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private WeaponSettings _weaponSettings;
        [SerializeField] private EnemySettings _enemySettings;
        [SerializeField] private CursorSetting _cursorSetting;
        [SerializeField] private Transform _spawnEnemyPosition;
        [SerializeField] private Bullet _bulletPrefab;

        public override void InstallBindings()
        {
            // Регистрация WeaponProvider и IWeaponRoot
            Container.Bind<WeaponProvider>().AsSingle().WithArguments(_weaponSettings);
            Container.Bind<EnemyProvider>().AsSingle().WithArguments(_enemySettings);
            Container.Bind<IWeaponRoot>().FromComponentInHierarchy().AsSingle();

            // Регистрация WeaponFactory
            Container.Bind<WeaponFactory>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle().WithArguments(_spawnEnemyPosition);
            
            // Регистрация PlayerController
            Container.BindInterfacesAndSelfTo<PlayerWeaponController>().AsSingle();

            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EnemySpawner>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
            
            // Регистрация PlayerMoveController и передача PlayerController
            Container.BindInterfacesAndSelfTo<PlayerMoveController>().AsSingle();
            
            // Регистрация CursorChanger
            Container.BindInterfacesAndSelfTo<CursorChanger>().AsSingle().WithArguments(_cursorSetting);
            
            Container.Bind<MonoBehaviour>().FromInstance(this).AsSingle();
            
            Container.Bind<Bullet>().FromInstance(_bulletPrefab).AsSingle();
            
        }
    }
}