using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnTree : MonoBehaviour {

	[Range (1, 10)]
	public float spawnRate;
	public GameObject prefab;
	public TreeController tree;
	public List<Transform> spawnPoints = new List<Transform>();

	private float spawnTimer;
	private int firstSpawn;
	private int randomX;

	

	void Start () {
		
		firstSpawn = Random.Range(2,5);
		spawnTimer = spawnRate;

		for (int i = 0; i < firstSpawn; i++) {
			randomX = Random.Range(0, 8);
			Spawn(spawnPoints[randomX]);
		}
	}

	void Update () {

		float treesInScene = GameObject.FindGameObjectsWithTag("Tree").Length;

		if (spawnTimer <= 0) {

			if (treesInScene < 2) {
				randomX = Random.Range(0, 8);
				Spawn(spawnPoints[randomX]);
			}
			spawnTimer = spawnRate;
		}

		else {
			spawnTimer -= Time.deltaTime;
		}
	}

	void Spawn (Transform spawnPoint) {

		RaycastHit2D hit = Physics2D.Raycast(new Vector2(spawnPoint.position.x, transform.position.y), Vector2.down);

		if (hit && hit.collider.CompareTag("Ground")) {
			Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
		}
	}
}