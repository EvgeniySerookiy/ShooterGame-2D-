using System;
using System.Collections.Generic;
using ProjectAssets.Scripts.Weapon;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ProjectAssets.Scripts
{
    public class PlayerWeaponController : IInitializable
    {
        private readonly WeaponFactory _weaponFactory;
        private Dictionary<WeaponType, WeaponController> _weapons;
        private WeaponController _weaponController;
        private WeaponType _weaponType;
        
        public PlayerWeaponController(WeaponFactory weaponFactory)
        {
            Debug.Log("PlayerController");
            _weaponFactory = weaponFactory;
        }
        
        public void Initialize()
        {
            _weapons = _weaponFactory.CreateAllWeapons();
            foreach (var weapon in _weapons.Values)
            {
                weapon.SetActive(false);
            }

            _weaponType = GetRandomWeaponType();
            _weaponController = _weapons[_weaponType];
            _weaponController.SetActive(true);
        }

        private WeaponType GetRandomWeaponType()
        {
            return (WeaponType)Random.Range(0, Enum.GetValues(typeof(WeaponType)).Length);
        }
        
        public void Fire()
        {
            _weaponController.Fire();
            Debug.Log(_weaponController.Damage);
            Debug.Log(_weaponController.FireRate);
        }

        public void StopFire()
        {
            _weaponController.StopFire();
        }

        public void SwitchWeapons(WeaponType weaponType)
        {
            _weaponType = weaponType;
            _weaponController.SetActive(false);
            _weaponController.StopFire();
            
            _weaponController = _weapons[_weaponType];
            
            _weaponController.SetActive(true);
        }

        public WeaponType GetWeaponType()
        {
            return _weaponType;
        }
        
        public WeaponController GetActiveWeaponController()
        {
            return _weaponController;
        }
    }
}