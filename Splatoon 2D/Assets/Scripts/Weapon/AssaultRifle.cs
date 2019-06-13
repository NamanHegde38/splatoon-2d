using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AssaultRifle : MonoBehaviour {

	public Transform firePoint;

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}
    }

	void Shoot () {
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);
		
	}
}
