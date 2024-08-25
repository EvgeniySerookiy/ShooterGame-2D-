using ProjectAssets.Scripts.Weapon.Settings;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponProvider 
    {
        private readonly WeaponSettings _weaponSettings;
    
        public WeaponProvider (WeaponSettings weaponSettings)
        {
            _weaponSettings = weaponSettings;
        }
        
        public WeaponSetting GetWeapon(WeaponType weaponType)
        {
            return _weaponSettings.Weapons.Find(w => w.Type == weaponType);
        }
    }
}