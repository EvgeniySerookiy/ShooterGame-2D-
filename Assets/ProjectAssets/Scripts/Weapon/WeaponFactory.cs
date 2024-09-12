using System.Collections.Generic;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponFactory
    {
        private readonly WeaponProvider _weaponProvider;
        private readonly MultiRoot _multiRoot;
        private readonly MonoBehaviour _monoBehaviour;

        public WeaponFactory(WeaponProvider weaponProvider, MultiRoot multiRoot, MonoBehaviour monoBehaviour)
        {
            Debug.Log("WeaponFactory");
            _weaponProvider = weaponProvider;
            _multiRoot = multiRoot;
            _monoBehaviour = monoBehaviour;
        }
        
        public Dictionary<WeaponType, WeaponController> CreateAllWeapons()
        {
            return new Dictionary<WeaponType, WeaponController>
            {
                { WeaponType.Pistol, new PistolController(_weaponProvider, _multiRoot, _monoBehaviour) },
                { WeaponType.Rifle, new RifleController(_weaponProvider, _multiRoot, _monoBehaviour) },
                { WeaponType.Shotgun, new ShotgunController(_weaponProvider, _multiRoot, _monoBehaviour) }
            };
        }
    }
}