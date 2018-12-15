using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnBush : MonoBehaviour {

	public GameObject bush;
	public Transform parent;

	private int spawnRate;
	private float randomX;
	private float randomY;
	private Vector2 spawnPosition;
	private Vector2 spawnPoint;

	void Start () {
		spawnRate = Random.Range(2, 4);
		for (int i = 0; i < spawnRate; i++) {
			FindSpawn();
		}
	}

	void FindSpawn () {

		randomX = Random.Range(-40, 25);
		spawnPosition = new Vector2(randomX, 5);
		RaycastHit2D ray = Physics2D.Raycast(spawnPosition, -transform.up);

		if (ray.collider != null && ray.collider.tag == "Ground") {
				spawnPoint = ray.point;
				Instantiate(bush, spawnPoint, Quaternion.identity, parent);
		}

		else {
				FindSpawn();
		}
	}
}