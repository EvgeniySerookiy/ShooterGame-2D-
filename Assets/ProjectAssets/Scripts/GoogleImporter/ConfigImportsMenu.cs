using System.Threading.Tasks;
using ProjectAssets.Scripts.Enemy;
using ProjectAssets.Scripts.Enemy.Settings;
using UnityEngine;

namespace ProjectAssets.Scripts.GoogleImporter
{
    public class ConfigImportsMenu
    {
        private const string SPREADSHEET_ID = "1fuqLXq5uWQqj4SoHh1_IZhBjy1m-ERW_nhwpZ3ZsNcY";
        private const string ITEMS_SHEETS_NAME = "EnemySetting";
        private const string CREDENTIALS_PATH = "shootergame-2d-436908-db155484d1c2.json";
        private EnemyListSettings _enemyListSettings;
        
        public async Task LoadItemmsEnemySetting()
        {
            var sheetsImporter = new GoogleSheetsImporter(CREDENTIALS_PATH, SPREADSHEET_ID);
            
            _enemyListSettings = new EnemyListSettings();
            var itemsParser = new ItemsSettingsParser(_enemyListSettings);
            
            await sheetsImporter.DawnloadAndParseSheet(ITEMS_SHEETS_NAME, itemsParser);
        }
        
        public void UpdateEnemySettingFromJson(EnemySetting enemySetting)
        {
            foreach (var setting in _enemyListSettings.Enemies)
            {
                if (setting.Type == enemySetting.Type)
                {
                    enemySetting.SetHealth(setting.Health);
                    enemySetting.SetDamage(setting.Damage);
                    enemySetting.SetSpeed(setting.Speed);
                    enemySetting.SetHealthRatio(setting.HealthRatio);
                    enemySetting.SetDamageRatio(setting.DamageRatio);
                    enemySetting.SetSpeedRatio(setting.SpeedRatio);
                }
            }
        }
    }
}