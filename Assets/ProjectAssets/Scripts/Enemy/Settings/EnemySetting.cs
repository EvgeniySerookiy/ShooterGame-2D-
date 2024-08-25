using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Enemy.Settings
{
    [Serializable]
    public class EnemySetting
    {
        [field: SerializeField] public EnemyType Type{ get; private set; }
        [field: SerializeField] public EnemyView ViewPrefab { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float AttackFrequency { get; private set; }
    }
}