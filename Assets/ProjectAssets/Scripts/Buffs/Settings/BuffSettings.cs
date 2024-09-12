using System.Collections.Generic;
using UnityEngine;

namespace ProjectAssets.Scripts.Buffs.Settings
{
    [CreateAssetMenu (menuName = "Settings/BuffSettings", fileName = "WeaponSettings")]
    public class BuffSettings : ScriptableObject
    {
        [field: SerializeField] public List<BuffSetting> Buffs { get; private set; }
    }
}