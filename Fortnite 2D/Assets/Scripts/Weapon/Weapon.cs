using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    
	public float offset;
	public Camera mainCam;
	public GameObject player;
    
    void Update() {

		Vector3 difference = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Deg2Rad;
		transform.rotation = Quaternion.Euler(0f, 0f, (rotZ + offset) * 3000);
		transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1f);
    }
}
