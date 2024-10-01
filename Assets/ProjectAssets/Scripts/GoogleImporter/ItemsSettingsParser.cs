using System;
using ProjectAssets.Scripts.Enemy;
using ProjectAssets.Scripts.Enemy.Settings;

namespace ProjectAssets.Scripts.GoogleImporter
{
    public class ItemsSettingsParser : IGoogleSheetParser
    {
        private readonly EnemyListSettings _enemyListSettings;
        private EnemySetting _currentEnemySetting;

        public ItemsSettingsParser(EnemyListSettings enemyListSettings)
        {
            _enemyListSettings = enemyListSettings;
        }
        
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Type":
                    _currentEnemySetting = new EnemySetting
                    {
                        Type = Enum.Parse<EnemyType>(token)
                    };
                    _enemyListSettings.Enemies.Add(_currentEnemySetting);
                    break;

                case "Health":
                    _currentEnemySetting.SetHealth(float.Parse(token));
                    break;

                case "HealthRatio":
                    _currentEnemySetting.SetHealthRatio(float.Parse(token));
                    break;

                case "Damage":
                    _currentEnemySetting.SetDamage(float.Parse(token));
                    break;

                case "DamageRatio":
                    _currentEnemySetting.SetDamageRatio(float.Parse(token));
                    break;

                case "Speed":
                    _currentEnemySetting.SetSpeed(float.Parse(token));
                    break;

                case "SpeedRatio":
                    _currentEnemySetting.SetSpeedRatio(float.Parse(token));
                    break;

                default:
                    throw new Exception($"Invalid header {header}.");
            }
        }
    }
}