using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class TreeController : MonoBehaviour {

	public float animCooldown = 0.05f;
	public float health = 100;
	public bool damaged = false;
	public Sprite[] sprite;

	private float animTimer = 0f;
	private SpriteRenderer spriteRenderer;
	private Animator anim;

	void Start () {

		anim = GetComponent<Animator>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		spriteRenderer.sprite = sprite[Random.Range(0, 3)];
	}

	void Update () {

		Cooldown();
		Destroy();

	}

	public void Damage (int damage) {

		health -= damage;
		animTimer = animCooldown;
		damaged = true;
	}

	public void Destroy () {

		if (health <= 0) {
			Destroy(gameObject);
		}
	}

	void Cooldown () {

		if (animTimer > 0) {
			animTimer -= Time.deltaTime;
		}

		else {
			damaged = false;
		}

		anim.SetBool("Damaged", damaged);
	}
}