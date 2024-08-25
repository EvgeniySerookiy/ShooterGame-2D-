using System.Collections.Generic;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using ProjectAssets.Scripts.Weapon.WeaponRoot;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponFactory
    {
        private readonly WeaponProvider _weaponProvider;
        private readonly IWeaponRoot _weaponRoot;
        private readonly MonoBehaviour _monoBehaviour;

        public WeaponFactory(WeaponProvider weaponProvider, IWeaponRoot weaponRoot, MonoBehaviour monoBehaviour)
        {
            Debug.Log("WeaponFactory");
            _weaponProvider = weaponProvider;
            _weaponRoot = weaponRoot;
            _monoBehaviour = monoBehaviour;
        }
        
        public List<WeaponController> CreateAllWeapons()
        {
            return new List<WeaponController>
            {
                new PistolController(_weaponProvider, _weaponRoot, _monoBehaviour),
                new RifleController(_weaponProvider, _weaponRoot, _monoBehaviour),
                new ShotgunController(_weaponProvider, _weaponRoot, _monoBehaviour)
            };
        }
    }
}