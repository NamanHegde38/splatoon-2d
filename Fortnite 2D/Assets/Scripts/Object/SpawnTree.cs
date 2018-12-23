using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnTree : MonoBehaviour {

	private float spawnTimer;
	public GameObject prefab;
	public Transform parent;
	public Tree tree;

	private int randomX;
	private int spawnRate;
	private bool nextSpawn;
	private RaycastHit2D spawnPosition;
	private Vector2 spawnPoint;

	List<RaycastHit2D> spawnSpot = new List<RaycastHit2D>();

	void Start () {

		spawnRate = Random.Range(2, 4);
		for (int i = 0; i < spawnRate; i++) {
			FirstSpawn();
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

		Vector2 spawnPoint1 = new Vector2 (-33, 10);
		Vector2 spawnPoint2 = new Vector2 (-30, 10);
		Vector2 spawnPoint3 = new Vector2(-25, 10);
		Vector2 spawnPoint4 = new Vector2(-20, 10);
		Vector2 spawnPoint5 = new Vector2(-15, 10);
		Vector2 spawnPoint6 = new Vector2(-10, 10);
		Vector2 spawnPoint7 = new Vector2(-5, 10);
		Vector2 spawnPoint8 = new Vector2(0, 10);
		Vector2 spawnPoint9 = new Vector2(5, 10);
		Vector2 spawnPoint10 = new Vector2(10, 10);

		RaycastHit2D ray1 = Physics2D.Raycast(spawnPoint1, Vector2.down);
		RaycastHit2D ray2 = Physics2D.Raycast(spawnPoint2, Vector2.down);
		RaycastHit2D ray3 = Physics2D.Raycast(spawnPoint3, Vector2.down);
		RaycastHit2D ray4 = Physics2D.Raycast(spawnPoint4, Vector2.down);
		RaycastHit2D ray5 = Physics2D.Raycast(spawnPoint5, Vector2.down);
		RaycastHit2D ray6 = Physics2D.Raycast(spawnPoint6, Vector2.down);
		RaycastHit2D ray7 = Physics2D.Raycast(spawnPoint7, Vector2.down);
		RaycastHit2D ray8 = Physics2D.Raycast(spawnPoint8, Vector2.down);
		RaycastHit2D ray9 = Physics2D.Raycast(spawnPoint9, Vector2.down);
		RaycastHit2D ray10 = Physics2D.Raycast(spawnPoint10, Vector2.down);

		spawnSpot.Add(ray1);
		spawnSpot.Add(ray2);
		spawnSpot.Add(ray3);
		spawnSpot.Add(ray4);
		spawnSpot.Add(ray5);
		spawnSpot.Add(ray6);
		spawnSpot.Add(ray7);
		spawnSpot.Add(ray8);
		spawnSpot.Add(ray9);
		spawnSpot.Add(ray10);

		spawnPosition = spawnSpot[randomX];

		Spawn();
	}

	void Spawn () {

		if (spawnPosition.collider != null && spawnPosition.collider.tag == "Ground") {
			Instantiate(prefab, spawnPosition.point, Quaternion.identity, parent);
			nextSpawn = false;
		}
	}

	void FirstSpawn () {

		randomX = Random.Range(-40, 25);
		Vector2 spawnPos = new Vector2(randomX, 5);
		RaycastHit2D ray = Physics2D.Raycast(spawnPos, -transform.up);

		if (ray.collider != null && ray.collider.tag == "Ground") {
			spawnPoint = ray.point;
			Instantiate(tree, spawnPoint, Quaternion.identity, parent);
		}

		else {
			FindSpawn();
		}
	}
}