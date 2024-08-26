using UnityEngine;

namespace ProjectAssets.Scripts.PlayerCharacter
{
    [CreateAssetMenu(menuName = "Settings/PlayerSetting", fileName = "PlayerSetting")]
    public class PlayerSetting : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
    }
}