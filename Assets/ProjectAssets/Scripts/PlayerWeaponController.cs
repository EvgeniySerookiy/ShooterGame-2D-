using System;
using System.Collections.Generic;
using ProjectAssets.Scripts.Weapon;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using Zenject;
using Random = UnityEngine.Random;

namespace ProjectAssets.Scripts
{
    public class PlayerWeaponController : IInitializable
    {
        public WeaponController WeaponController { get; private set; }
        public WeaponType WeaponType { get; private set; }
        
        private readonly Dictionary<WeaponType, WeaponController> _weapons;
        
        
        public PlayerWeaponController(Dictionary<WeaponType, WeaponController> weapons)
        {
            _weapons = weapons;
        }
        
        public void Initialize()
        {
            WeaponType = GetRandomWeaponType();
            WeaponController = _weapons[WeaponType];
            WeaponController.SetActive(true);
        }

        private WeaponType GetRandomWeaponType()
        {
            return (WeaponType)Random.Range(0, Enum.GetValues(typeof(WeaponType)).Length);
        }
        
        public void Fire()
        {
            WeaponController.Fire();
        }

        public void StopFire()
        {
            WeaponController.StopFire();
        }

        public void SwitchWeapons(WeaponType weaponType)
        {
            WeaponType = weaponType;
            WeaponController.SetActive(false);
            WeaponController.StopFire();
            
            WeaponController = _weapons[WeaponType];
            
            WeaponController.SetActive(true);
        }
    }
}