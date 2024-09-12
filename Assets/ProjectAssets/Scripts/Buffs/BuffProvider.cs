using ProjectAssets.Scripts.Buffs.Settings;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffProvider
    {
        private readonly BuffSettings _buffSettings;

        public BuffProvider(BuffSettings buffSettings)
        {
            _buffSettings = buffSettings;
        }

        public BuffSetting GetBuff(BuffType buffType)
        {
            return _buffSettings.Buffs.Find(b => b.Type == buffType);
        }
    }
}