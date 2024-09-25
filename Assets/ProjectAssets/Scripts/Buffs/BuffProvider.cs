using System.Collections.Generic;
using System.Linq;
using ProjectAssets.Scripts.Buffs.Settings;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffProvider
    {
        private readonly Dictionary<BuffType, BuffSetting> _buffListSettings;

        public BuffProvider(BuffListSettings buffListSettings)
        {
            _buffListSettings = buffListSettings
                .Buffs
                .ToDictionary(b => b.Type, b => b);
        }

        public BuffSetting GetBuff(BuffType buffType)
        {
            return _buffListSettings[buffType];
        }
    }
}