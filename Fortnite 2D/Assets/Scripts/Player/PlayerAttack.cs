using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class PlayerAttack : MonoBehaviour {

	public int damage;
	public float startTimeBtwAttack;
	public float attackRange;
	public LayerMask whatIsEnemies;
	public Transform attackPos;

	float timeBtwAttack = 0;

	Animator anim;

	void Start() {

		anim = GetComponent<Animator>();
	}

	void Update() {

		if (timeBtwAttack <= 0) {

			if (Input.GetButton("Fire1")) {
				anim.SetTrigger("Attacking");
				Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
				for (int i = 0; i < enemiesToDamage.Length; i++) {
					enemiesToDamage[i].GetComponent<TreeController>().TakeDamage(damage);
				}
				timeBtwAttack = startTimeBtwAttack;
			}
		}

		else {
			timeBtwAttack -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected() {

		Vector2 clickedPosition;

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			clickedPosition = Input.mousePosition;
		}

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPos.position, attackRange);
	}

	

}