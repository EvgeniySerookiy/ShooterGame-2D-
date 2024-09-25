using System.Collections.Generic;
using System.Linq;
using ProjectAssets.Scripts.Weapon.Settings;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponProvider 
    {
        private readonly Dictionary<WeaponType, WeaponSetting>_weaponListSettings;
    
        public WeaponProvider (WeaponListSettings weaponListSettings)
        {
            _weaponListSettings = weaponListSettings
                .Weapons
                .ToDictionary(w => w.Type, w => w);
        }
        
        public WeaponSetting GetWeapon(WeaponType weaponType)
        {
            return _weaponListSettings[weaponType];
        }
    }
}