using System.Collections.Generic;
using ProjectAssets.Scripts.Weapon;
using ProjectAssets.Scripts.Weapon.WeaponControllers;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts
{
    public class PlayerWeaponController : IInitializable
    {
        private readonly WeaponFactory _weaponFactory;
        public List<WeaponController> Weapons { get; private set; }
        private WeaponController _activeWeapon;
        
        public PlayerWeaponController(WeaponFactory weaponFactory)
        {
            Debug.Log("PlayerController");
            _weaponFactory = weaponFactory;
        }
        
        public void Initialize()
        {
            Weapons = _weaponFactory.CreateAllWeapons();
            foreach (var weapon in Weapons)
            {
                weapon.SetActive(false);
            }

            _activeWeapon = Weapons[GetRandomIndex()];
            _activeWeapon.SetActive(true);
        }

        private int GetRandomIndex()
        {
            return Random.Range(0, Weapons.Count);
        }
        
        public void Fire()
        {
            _activeWeapon?.Fire();
        }
    }
}