using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnTree : MonoBehaviour {

	public GameObject prefab;
	public Transform parent;

	private float randomX;
	private float randomY;
	private int spawnRate;
	private bool nextSpawn;
	private Vector2 spawnPosition;
	private Vector2 spawnPoint;
	private RaycastHit2D ray;
	private Tree tree;

	List<Vector2> spawnSpot = new List<Vector2>();

	void Start () {

		tree = GetComponentInChildren<Tree>();
		spawnRate = Random.Range(2, 4);
		for (int i = 0; i < spawnRate; i++) {
			FirstSpawn();
		}
	}

	void Update() {
		CheckSpawn();
	}

	void CheckSpawn () {

		if (tree.health <= 0) {
			nextSpawn = true;
		}

		if (nextSpawn) {
			FindSpawn();
		}
	}

	public void FindSpawn () {

		randomX = Random.Range(-33, 10);
		spawnPosition = new Vector2(randomX, 5);
		ray = Physics2D.Raycast(spawnPosition, -transform.up);
		spawnSpot.Add(ray.point);
		float distance = Mathf.Abs(Vector2.Distance(spawnSpot[0], ray.point));

		if (distance <= 4) {
				FindSpawn();
		}
			
		else {
				Spawn();
		}
	}

	void Spawn () {

		if (ray.collider != null && ray.collider.tag == "Ground") {
			spawnPoint = ray.point;
			Instantiate(prefab, spawnPoint, Quaternion.identity, parent);
		}

		else {
			FindSpawn();
		}
	}

	void FirstSpawn () {
		randomX = Random.Range(-40, 25);
		spawnPosition = new Vector2(randomX, 5);
		RaycastHit2D ray = Physics2D.Raycast(spawnPosition, -transform.up);

		if (ray.collider != null && ray.collider.tag == "Ground") {
			spawnPoint = ray.point;
			Instantiate(tree, spawnPoint, Quaternion.identity, parent);
		}

		else {
			FindSpawn();
		}
	}
}