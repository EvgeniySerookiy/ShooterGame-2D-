using System.Collections.Generic;
using UnityEngine;

namespace ProjectAssets.Scripts.Enemy.Settings
{
    [CreateAssetMenu(menuName = "Settings/EnemyListSettings", fileName = "EnemyListSettings")]
    public class EnemyListSettings : ScriptableObject
    {
        [field: SerializeField] public List<EnemySetting> Enemies { get; private set; }
    }
}