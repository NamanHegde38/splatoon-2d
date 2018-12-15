using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class PlayerHarvest : MonoBehaviour {

	public float harvestCooldown = 0.3f;
	public Collider2D harvestTrigger;
	public KeyCode harvest;

	private float harvestTimer = 0f;
	private bool harvesting = false;
	private Animator anim;

	void Awake () {

		anim = GetComponent<Animator>();
	}

	void Start () {

		harvestTrigger.enabled = false;
	}
	
	void Update () {

		Harvest();
	}

	void Harvest () {

		if (Input.GetKeyDown(harvest)) {

			harvesting = true;
			harvestTimer = harvestCooldown;
			harvestTrigger.enabled = true;
		}

		if (harvesting) {

			if (harvestTimer > 0) {
				harvestTimer -= Time.deltaTime;
			}

			else {
				harvesting = false;
				harvestTrigger.enabled = false;
			}
		}

		anim.SetBool("Harvesting", harvesting);
	}
}