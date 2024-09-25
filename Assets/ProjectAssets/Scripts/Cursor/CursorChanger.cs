using ProjectAssets.Scripts.Cursor.Settings;
using UnityEngine;
using Zenject;

namespace ProjectAssets.Scripts.Cursor
{
    public class CursorChanger : IInitializable
    {
        private Texture2D _customCursorTexture;
        private Vector2 _hotSpot = new(32f, 32f);
        private CursorMode _cursorMode = CursorMode.ForceSoftware;

        public CursorChanger(CursorSetting cursorSetting)
        {
            _customCursorTexture = cursorSetting.customCursorTexture;
        }
        
        public void Initialize()
        {
            UnityEngine.Cursor.SetCursor(_customCursorTexture, _hotSpot, _cursorMode);
        }
    }
}