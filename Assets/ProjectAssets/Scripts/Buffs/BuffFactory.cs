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

        public BuffFactory(BuffProvider buffProvider, MonoBehaviour monoBehaviour, MultiRoot multiRoot)
        {
            _buffProvider = buffProvider;
            _monoBehaviour = monoBehaviour;
            _multiRoot = multiRoot;
        }

        public BuffController CreateBuff(BuffType buffType, Vector3 position)
        {
            return new BuffController(_buffProvider, _multiRoot, buffType, position, _monoBehaviour);
        }
    }
}