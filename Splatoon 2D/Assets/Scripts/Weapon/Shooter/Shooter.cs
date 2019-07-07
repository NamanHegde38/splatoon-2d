using UnityEngine;

namespace Assets.Scripts.Weapon.Shooter {
	public class Shooter : MonoBehaviour {
		
		[SerializeField] private float offset;

		void Update() {
		
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

			if (transform.rotation.z == 360f) {
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
	}
}
