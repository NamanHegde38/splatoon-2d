using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnTree : MonoBehaviour {

	public GameObject prefab;
	public Transform parent;
	public TreeController tree;
	public List<Vector2> spawnPoints = new List<Vector2>();

	private float spawnTimer;
	private int randomX;
	private int spawnRate;
	private bool nextSpawn;

	

	void Start () {

		spawnRate = Random.Range(2, 4);
		for (int i = 0; i < spawnRate; i++) {
			Spawn();
		}
	}

	void Update() {
		CheckSpawn();
	}

	void CheckSpawn () {

		if (parent.childCount < 2) {
			nextSpawn = true;
		}

		if (nextSpawn) {
			FindSpawn();
		}
	}

	public void FindSpawn () {

		randomX = Random.Range(0, 9);

		Spawn();
	}

	void Spawn () {

		for (int i = 0; i < spawnPoints.Count; i++) {
			if (spawnPoints[randomX] == spawnPoints[i]) {
				InstantiatePrefab(spawnPoints[i]);
			}
		}
	}

	void InstantiatePrefab(Vector2 spawnPoint) {

		RaycastHit2D hit = Physics2D.Raycast(spawnPoint, Vector2.down);

		if (hit.collider != null && hit.collider.CompareTag("Ground")) {
			Instantiate(prefab, hit.point, Quaternion.identity, parent);
			nextSpawn = false;

		}
	}
}