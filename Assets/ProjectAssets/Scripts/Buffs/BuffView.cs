using ProjectAssets.Scripts.PlayerCharacter;
using UnityEngine;

namespace ProjectAssets.Scripts.Buffs
{
    public class BuffView : MonoBehaviour
    {
        private BuffSpawner _buffSpawner;
        public BuffType BuffType { get; private set; }
    
        public void Initialize(BuffSpawner buffSpawner, BuffType buffType)
        {
            _buffSpawner = buffSpawner;
            BuffType = buffType;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player _))
            {
                _buffSpawner.RemoveBuff(this);
            }
        }

        public void DestroyBuff()
        {
            Destroy(gameObject);
        }
    }
}