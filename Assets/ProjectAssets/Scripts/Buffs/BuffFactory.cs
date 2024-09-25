using UnityEngine;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffFactory
    {
        private readonly BuffProvider _buffProvider;
        private readonly Transform _buffRoot;

        public BuffFactory(BuffProvider buffProvider, Transform buffRoot)
        {
            _buffProvider = buffProvider;
            _buffRoot = buffRoot;
        }

        public BuffView CreateBuff(BuffType buffType, Vector3 position)
        {
            var buffSetting = _buffProvider.GetBuff(buffType);
            return Object.Instantiate(buffSetting.ViewPrefab, position, Quaternion.identity, _buffRoot);
        }
    }
}