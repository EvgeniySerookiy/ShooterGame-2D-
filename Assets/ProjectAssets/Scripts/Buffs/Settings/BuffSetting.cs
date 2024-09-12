using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Buffs.Settings
{
    [Serializable]
    public class BuffSetting
    {
        [field: SerializeField] public BuffType Type{ get; private set; }
        [field: SerializeField] public float Value{ get; private set; }
        [field: SerializeField] public BuffView ViewPrefab{ get; private set; }
    }
}