using UnityEngine;

namespace ProjectAssets.Scripts.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [field: SerializeField] public Transform Muzzle { get; private set; }
        
        private void Update()
        {
            WeaponRotation();
        }

        private void WeaponRotation()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
            var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
            transform.rotation = Quaternion.Slerp(transform.rotation,
                rotation, _rotationSpeed * Time.deltaTime);
            
            FlipYWeapon();
        }

        private void FlipYWeapon()
        {
            _spriteRenderer.flipY = transform.rotation.eulerAngles.z <= 270f && 
                                    transform.rotation.eulerAngles.z >= 90f;
        }
    }
}