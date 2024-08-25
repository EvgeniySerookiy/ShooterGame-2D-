using System.Collections.Generic;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.Settings
{
    [CreateAssetMenu (menuName = "Settings/WeaponSettings", fileName = "WeaponSettings")]
    public class WeaponSettings : ScriptableObject
    {
        [field: SerializeField] public List<WeaponSetting> Weapons { get; private set; }
    }
}