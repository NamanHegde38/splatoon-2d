using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnBush : MonoBehaviour {

	public GameObject prefab;
	public List<Transform> spawnPoints = new List<Transform>();

	private int firstSpawn;
	private int randomX;

	void Start() {

		firstSpawn = Random.Range(3, 6);

		for (int i = 0; i < firstSpawn; i++) {
			randomX = Random.Range(0, 8);
			Spawn(spawnPoints[randomX]);
		}
	}

	void Spawn(Transform spawnPoint) {

		RaycastHit2D hit = Physics2D.Raycast(new Vector2(spawnPoint.position.x, transform.position.y), Vector2.down);

		if (hit && hit.collider.CompareTag("Ground")) {
			Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
		}
	}
}