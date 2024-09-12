using ProjectAssets.Scripts.Buffs.Settings;
using UnityEngine;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffController
    {
        private readonly BuffProvider _buffProvider;
        private readonly BuffSetting _buffSetting;

        public BuffController(BuffProvider buffProvider, MultiRoot multiRoot, BuffType buffType, Vector3 position, MonoBehaviour monoBehaviour)
        {
            _buffProvider = buffProvider;
            _buffSetting = _buffProvider.GetBuff(buffType);

            // Создаём баф в указанной позиции
            var buff = Object.Instantiate(_buffSetting.ViewPrefab, position, Quaternion.identity, multiRoot.GetRootForBuff());
        }
    }
}