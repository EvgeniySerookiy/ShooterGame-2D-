using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponControllers
{
    public class RifleController : WeaponController
    {
        public RifleController(WeaponProvider weaponProvider, Transform weaponRoot, CoroutineLauncher coroutineLauncher) :
            base(weaponProvider, weaponRoot, coroutineLauncher)
        {
        }

        public override WeaponType WeaponType => WeaponType.Rifle;
    }
}