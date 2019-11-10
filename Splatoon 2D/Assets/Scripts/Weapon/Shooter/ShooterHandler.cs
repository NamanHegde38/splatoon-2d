#pragma warning disable 649

using UnityEngine;

namespace Weapon.Shooter {
    
    [RequireComponent(typeof(WeaponInput))]
    [RequireComponent(typeof(Shooter))]
    public class ShooterHandler : MonoBehaviour {

        private Camera _mainCamera;
        public Shooter shooter;
        [SerializeField] private float offset;

        [SerializeField] private GameObject projectile;
        [SerializeField] private Transform firePoint;
        
        private void Awake() {
            _mainCamera = Camera.main;
        }

        private void Update() {
            var difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }

        public void Shoot() {
            // ReSharper disable once Unity.InefficientPropertyAccess
            Instantiate(projectile, firePoint.position, transform.rotation);
        }
    }
}
