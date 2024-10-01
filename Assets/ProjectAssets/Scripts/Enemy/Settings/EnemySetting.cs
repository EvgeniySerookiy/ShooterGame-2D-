using System;
using ProjectAssets.Scripts.Blood;
using UnityEngine;


namespace ProjectAssets.Scripts.Enemy.Settings
{
    [Serializable]
    public class EnemySetting
    {
        [field: SerializeField] public EnemyType Type { get; set; }
        [field: SerializeField] public EnemyView ViewPrefab;
        [field: SerializeField] public BloodEffectParticle BloodEffectParticle;
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float HealthRatio { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DamageRatio { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SpeedRatio { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        
        
        public EnemySetting Clone()
        {
            return (EnemySetting) MemberwiseClone();
        }
        
        public void SetHealth(float health)
        {
            Health = Mathf.Max(0, health);
        }
        
        public void SetDamage(float damage)
        {
            Damage = Mathf.Max(0, damage);
        }
        
        public void SetSpeed(float speed)
        {
            Speed = Mathf.Max(0, speed);
        }
        
        public void SetHealthRatio(float healthRatio)
        {
            HealthRatio = Mathf.Max(0, healthRatio);
        }
        
        public void SetDamageRatio(float damageRatio)
        {
            DamageRatio = Mathf.Max(0, damageRatio);
        }
        
        public void SetSpeedRatio(float speedRatio)
        {
            SpeedRatio = Mathf.Max(0, speedRatio);
        }
    }
}