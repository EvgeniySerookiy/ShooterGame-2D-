using ProjectAssets.Scripts.Root;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffFactory
    {
        private readonly BuffProvider _buffProvider;
        private readonly DiContainer _container;
        private readonly MonoBehaviour _monoBehaviour;
        private readonly MultiRoot _multiRoot;

        public BuffFactory(BuffProvider buffProvider, MultiRoot multiRoot)
        {
            _buffProvider = buffProvider;
            _multiRoot = multiRoot;
        }

        public BuffView CreateBuff(BuffType buffType, Vector3 position)
        {
            var buffSetting = _buffProvider.GetBuff(buffType);
            return Object.Instantiate(buffSetting.ViewPrefab, position, Quaternion.identity, _multiRoot.GetRootForBuff()).GetComponent<BuffView>();
        }
    }
}