using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class PlayerHarvest : MonoBehaviour {

	public float harvestCooldown = 0.3f;
	public GameObject harvestTrigger;
	public Transform parent;
	public KeyCode harvest;

	private float harvestTimer = 0f;
	private bool harvesting = false;
	private GameObject spawnedTrigger;
	private Animator anim;

	void Awake () {

		anim = GetComponent<Animator>();
	}
	
	void Update () {

		Harvest();
	}

	void Harvest () {

		if (Input.GetKeyDown(harvest)) {

			harvesting = true;
			harvestTimer = harvestCooldown;
			Instantiate (harvestTrigger, transform.position, Quaternion.identity, parent);
			spawnedTrigger = (GameObject) Instantiate (harvestTrigger, transform.position, Quaternion.identity, parent);
		}
		
		if (harvesting) {

			if (harvestTimer > 0) {
				harvestTimer -= Time.deltaTime;
			}

			else {
				harvesting = false;
				Destroy (spawnedTrigger);
			}
		}

		anim.SetBool("Harvesting", harvesting);
	}
}