using System;
using ProjectAssets.Scripts.Bullets.Settings;
using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.Settings
{
    [Serializable]
    public class WeaponSetting
    {
        [field: SerializeField] public WeaponType Type{ get; private set; }
        [field: SerializeField] public WeaponView ViewPrefab{ get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DamageRatio { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public float FireRateRatio { get; private set; }
        [field: SerializeField] public BulletSetting BulletSetting { get; private set; }
        [field: SerializeField] public Sprite[] SpritesMuzzleFlash { get; private set; }
        
    }
}