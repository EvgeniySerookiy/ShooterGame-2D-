using System.Collections.Generic;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.Settings
{
    [CreateAssetMenu (menuName = "Settings/WeaponListSettings", fileName = "WeaponListSettings")]
    public class WeaponListSettings : ScriptableObject
    {
        [field: SerializeField] public List<WeaponSetting> Weapons { get; private set; }
    }
}