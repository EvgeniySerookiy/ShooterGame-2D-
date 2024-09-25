using System.Collections.Generic;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponFactory : IFactory<Dictionary<WeaponType, WeaponController>>
    {
        private readonly WeaponProvider _weaponProvider;
        private readonly Transform _weaponRoot;
        private readonly CoroutineLauncher _coroutineLauncher;

        public WeaponFactory(WeaponProvider weaponProvider, Transform weaponRoot, CoroutineLauncher coroutineLauncher)
        {
            _weaponProvider = weaponProvider;
            _weaponRoot = weaponRoot;
            _coroutineLauncher = coroutineLauncher;
        }

        public Dictionary<WeaponType, WeaponController> Create()
        {
            Dictionary<WeaponType, WeaponController> weaponControllers = new()
            {
                { WeaponType.Pistol, new PistolController(_weaponProvider, _weaponRoot, _coroutineLauncher) },
                { WeaponType.Rifle, new RifleController(_weaponProvider, _weaponRoot, _coroutineLauncher) },
                { WeaponType.Shotgun, new ShotgunController(_weaponProvider, _weaponRoot, _coroutineLauncher) }
            };

            DeactivateAllWeapons(weaponControllers);
            
            return weaponControllers;
        }

        private void DeactivateAllWeapons(Dictionary<WeaponType, WeaponController> weaponControllers)
        {
            foreach (var weaponController in weaponControllers)
            {
                weaponController.Value.SetActive(false);
            }
        }
    }
}