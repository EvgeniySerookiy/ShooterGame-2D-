using System.Collections.Generic;
using UnityEngine;

namespace ProjectAssets.Scripts.Buffs.Settings
{
    [CreateAssetMenu (menuName = "Settings/BuffListSettings", fileName = "BuffListSettings")]
    public class BuffListSettings : ScriptableObject
    {
        [field: SerializeField] public List<BuffSetting> Buffs { get; private set; }
        [field: SerializeField] public float BuffSpawnInterval { get; private set; }
        [field: SerializeField] public int MaxBuffCount { get; private set; }
    }
}