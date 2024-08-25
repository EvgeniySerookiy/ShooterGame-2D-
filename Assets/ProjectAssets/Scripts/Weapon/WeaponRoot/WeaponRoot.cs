using UnityEngine;

namespace ProjectAssets.Scripts.Weapon.WeaponRoot
{
    public class WeaponRoot : MonoBehaviour, IWeaponRoot
    {
        [field: SerializeField] public Transform Root { get; private set; }
    }
}