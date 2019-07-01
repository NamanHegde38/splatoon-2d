using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Weapon : MonoBehaviour {
    
	public float offset;

	private PlayerMovement _player;

	private void Start() {
		_player = GetComponentInParent<PlayerMovement>();
	}

	void Update() {
		
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

		if (transform.rotation.z == 360f) {
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}
}
