using UnityEngine;

namespace ProjectAssets.Scripts
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private Transform _upperLeftCorner;
        [SerializeField] private Transform _upperRightCorner;
        [SerializeField] private Transform _lowerLeftCorner;
        private Transform _positionsBuffs;

        public Transform GetPositions()
        {
            return _positionsBuffs;
        }
    
        public Vector2 GetRandomFreePositionInsideField(Vector2 size)
        {
            Vector2 newPosition;
        
            while (true)
            {
                var randomX = Random.Range(_upperLeftCorner.position.x, _upperRightCorner.position.x);
                var randomY = Random.Range(_upperLeftCorner.position.y, _lowerLeftCorner.position.y);
                newPosition = new Vector2(randomX, randomY);
            
                var overlabCollider = Physics2D.OverlapBox(newPosition, size, 0);
                if (overlabCollider == null)
                {
                    break;
                }
            }
            return newPosition;
        }
    }
}