using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class Tree : MonoBehaviour {

	public float health = 100;
	public Sprite[] sprite;

	private SpriteRenderer spriteRenderer;

	void Start () {

		spriteRenderer = GetComponent<SpriteRenderer>();

		spriteRenderer.sprite = sprite[Random.Range(0, 3)];
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