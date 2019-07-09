using UnityEngine;

namespace Weapon.Shooters {
	public class Shooter : MonoBehaviour {
    
		public float offset;
		private Camera _camera;

		private void Start() {
			_camera = Camera.main;
		}

		private void Update() {
			var difference = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

			if (transform.rotation.z == 360f) {
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
	}
}
