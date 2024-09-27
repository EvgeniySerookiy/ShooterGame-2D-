using System;
using ProjectAssets.Scripts.Blood;
using UnityEngine;
using Unity.Services.RemoteConfig;


namespace ProjectAssets.Scripts.Enemy.Settings
{
    [Serializable]
    public class EnemySetting
    {
        private struct UserAttributes { }
        private struct AppAttributes { }
        
        [field: SerializeField] public EnemyType Type{ get; private set; }
        [field: SerializeField] public EnemyView ViewPrefab { get; private set; }
        [field: SerializeField] public BloodEffectParticle BloodEffectParticle { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float HealthRatio { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float DamageRatio { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float SpeedRatio { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        
        public void UpdateEnemySettingsFromRemote()
        {
            RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
            
            string enemyTypeKey = Type.ToString();
            
            RemoteConfigService.Instance.appConfig.GetFloat($"{enemyTypeKey}_health", Health);
            RemoteConfigService.Instance.appConfig.GetFloat($"{enemyTypeKey}_damage", Damage);
            RemoteConfigService.Instance.appConfig.GetFloat($"{enemyTypeKey}_speed", Speed);
            RemoteConfigService.Instance.appConfig.GetFloat($"{enemyTypeKey}_healthRatio", HealthRatio);
            RemoteConfigService.Instance.appConfig.GetFloat($"{enemyTypeKey}_damageRatio", DamageRatio);
            RemoteConfigService.Instance.appConfig.GetFloat($"{enemyTypeKey}_speedRatio", SpeedRatio);
            
            Debug.Log($"{enemyTypeKey}_health");
            Debug.Log(Health);
            Debug.Log(Damage);
            Debug.Log(Speed);
        }
        
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
    }
}