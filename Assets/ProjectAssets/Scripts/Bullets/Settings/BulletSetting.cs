using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Bullets.Settings
{
    [Serializable]
    public class BulletSetting
    {
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public bool СanPenetrate { get; private set; }
        [field: SerializeField] public bool IsEnemyShooting { get; private set; }
        [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    }
}