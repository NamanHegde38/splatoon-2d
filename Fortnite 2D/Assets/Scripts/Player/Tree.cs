﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class Tree : MonoBehaviour {

	public float health = 100;


	void Start () {
		
		
	}
	
	void Update () {
		
		
	}

	public void Damage (int damage) {

		health -= damage;
		if (health <= 0) {
			Destroy();
		}
	}

	public void Destroy () {
		Destroy(gameObject);
	}
}