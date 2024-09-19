using UnityEngine;

namespace ProjectAssets.Scripts.Root
{
    public class MultiRoot : MonoBehaviour
    {
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private Transform _buffRoot;
    
        public Transform GetRootForWeapon()
        {
            return _weaponRoot;
        }
    
        public Transform GetRootForBuff()
        {
            return _buffRoot;
        }
    }
}