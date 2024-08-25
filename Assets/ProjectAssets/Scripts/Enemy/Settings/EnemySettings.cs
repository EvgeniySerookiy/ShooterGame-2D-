using System.Collections.Generic;
using UnityEngine;

namespace ProjectAssets.Scripts.Enemy.Settings
{
    [CreateAssetMenu(menuName = "Settings/EnemySettings", fileName = "EnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [field: SerializeField] public List<EnemySetting> Enemies { get; private set; }
    }
}