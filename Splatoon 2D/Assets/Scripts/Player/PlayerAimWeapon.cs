using System;
using UnityEngine;
using CodeMonkey.Utils;
// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace Player {
    public class PlayerAimWeapon : MonoBehaviour {

        private Transform _aimTransform;
        private Transform _firePoint;
        
        private void Awake() {
            _aimTransform = transform.Find("Aim");
            _firePoint = transform.Find("Fire Point");
        }

        private void Update() {
            HandleAiming();
        }
        
        private void HandleAiming() {
            var mousePosition = UtilsClass.GetMouseWorldPosition();
            var aimDirection = (mousePosition - transform.position).normalized;
            var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _aimTransform.eulerAngles = new Vector3(0, 0, angle);
            
        }
        
        private void Shoot() {
            throw new NotImplementedException();
        }
    }
}
