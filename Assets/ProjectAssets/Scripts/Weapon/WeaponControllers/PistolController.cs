using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class PistolController : WeaponController
    { 
        public PistolController(WeaponProvider weaponProvider, Transform weaponRoot, CoroutineLauncher coroutineLauncher) :
            base(weaponProvider, weaponRoot, coroutineLauncher)
        {
        }

        public override WeaponType GetWeaponType()
        {
            return WeaponType.Pistol;
        }
    }
}