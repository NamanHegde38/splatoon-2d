using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class HarvestTrigger : MonoBehaviour {

	public float damage = 20;

	void OnTriggerEnter2D (Collider2D col) {
		
		if (col.isTrigger != true && col.GetComponent<TreeController>()) {
			col.SendMessageUpwards("Damage", damage);
		}
	}

	public void Destroy () {

		Destroy(gameObject);
	}
}