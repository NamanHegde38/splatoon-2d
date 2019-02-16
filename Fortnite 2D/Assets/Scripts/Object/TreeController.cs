using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class TreeController : MonoBehaviour {

	public int health = 100;
	public List<Sprite> sprites = new List<Sprite>();

	private int randomSprite;
	private SpriteRenderer spriteRenderer;
	private Animator anim;


	void Start () {

		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponent<Animator>();
		randomSprite = Random.Range(0, sprites.Count - 1);
		spriteRenderer.sprite = sprites[randomSprite];
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