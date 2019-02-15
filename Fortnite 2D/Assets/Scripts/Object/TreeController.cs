using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class TreeController : MonoBehaviour {

	public int health = 100;

	private Animator anim;

	void Start () {

		anim = GetComponent<Animator>();
	}

	void Update () {

		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	public void TakeDamage (int damage) {
		health -= damage;
		anim.SetTrigger("Damaged");
	}



	
}