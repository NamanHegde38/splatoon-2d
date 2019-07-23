using UnityEngine;

namespace Weapon.Shooters {
	public class ShooterManager : MonoBehaviour {
    
		private Transform _transform;
		public float offset;
		private Camera _camera;

		public Shooter shooter;

		public GameObject firePoint;
		public GameObject projectile;

		private float _nextFire;

		private void Start() {
			SetComponents();
		}

		private void SetComponents() {
			_camera = Camera.main;
			_transform = transform;
		}

		private void Update() {
			PointTowardsMouse();
			GetFireInput();
		}

		private void PointTowardsMouse() {
			var difference = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

			if (transform.rotation.z == 360f) {
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}

		private void GetFireInput() {
			if (Input.GetButton("Fire1")) {
				if (_nextFire <= 0f) {
					var fireRate = shooter.fireRate / 60f;
					_nextFire = fireRate;
					Shoot();
				}
				else {
					_nextFire -= Time.deltaTime;
				}
			}
		}

		private void Shoot() {
			Instantiate(projectile, firePoint.transform.position, transform.rotation);
		}
	}
}
