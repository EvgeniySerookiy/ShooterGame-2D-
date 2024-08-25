using UnityEngine;

namespace ProjectAssets.Scripts.Cursor.Settings
{
    [CreateAssetMenu(menuName = "Settings/CursorSetting", fileName = "CursorSetting")]
    public class CursorSetting : ScriptableObject
    {
        [field: SerializeField] public Texture2D customCursorTexture { get; private set; }

    }
}