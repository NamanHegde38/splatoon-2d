using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class SpawnTree : MonoBehaviour {

	private float spawnTimer;
	public GameObject prefab;
	public Transform parent;
	public TreeController tree;

	private int randomX;
	private int spawnRate;
	private bool nextSpawn;
	private RaycastHit2D spawnPosition;
	private Vector2 spawnPoint;

	private Vector2 spawnPoint1;
	private Vector2 spawnPoint2;
	private Vector2 spawnPoint3;
	private Vector2 spawnPoint4;
	private Vector2 spawnPoint5;
	private Vector2 spawnPoint6;
	private Vector2 spawnPoint7;
	private Vector2 spawnPoint8;
	private Vector2 spawnPoint9;
	private Vector2 spawnPoint10;

	List<Vector2> spawnSpot = new List<Vector2>();

	void Start () {

		spawnRate = Random.Range(2, 4);
		for (int i = 0; i < spawnRate; i++) {
			FirstSpawn();
		}
	}

	void Update() {
		CheckSpawn();
	}

	void FirstSpawn() {

		spawnPoint1 = new Vector2(-25, 10);
		spawnPoint2 = new Vector2(-20, 10);
		spawnPoint3 = new Vector2(-15, 10);
		spawnPoint4 = new Vector2(-10, 10);
		spawnPoint5 = new Vector2(-5, 10);
		spawnPoint6 = new Vector2(0, 10);
		spawnPoint7 = new Vector2(5, 10);
		spawnPoint8 = new Vector2(10, 10);
		spawnPoint9 = new Vector2(15, 10);
		spawnPoint10 = new Vector2(18, 10);

		spawnSpot.Add(spawnPoint1);
		spawnSpot.Add(spawnPoint2);
		spawnSpot.Add(spawnPoint3);
		spawnSpot.Add(spawnPoint4);
		spawnSpot.Add(spawnPoint5);
		spawnSpot.Add(spawnPoint6);
		spawnSpot.Add(spawnPoint7);
		spawnSpot.Add(spawnPoint8);
		spawnSpot.Add(spawnPoint9);
		spawnSpot.Add(spawnPoint10);

		Spawn();
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

		spawnPoint1 = new Vector2 (-33, 10);
		spawnPoint2 = new Vector2 (-30, 10);
		spawnPoint3 = new Vector2(-25, 10);
		spawnPoint4 = new Vector2(-20, 10);
		spawnPoint5 = new Vector2(-15, 10);
		spawnPoint6 = new Vector2(-10, 10);
		spawnPoint7 = new Vector2(-5, 10);
		spawnPoint8 = new Vector2(0, 10);
		spawnPoint9 = new Vector2(5, 10);
		spawnPoint10 = new Vector2(10, 10);

		spawnSpot.Add(spawnPoint1);
		spawnSpot.Add(spawnPoint2);
		spawnSpot.Add(spawnPoint3);
		spawnSpot.Add(spawnPoint4);
		spawnSpot.Add(spawnPoint5);
		spawnSpot.Add(spawnPoint6);
		spawnSpot.Add(spawnPoint7);
		spawnSpot.Add(spawnPoint8);
		spawnSpot.Add(spawnPoint9);
		spawnSpot.Add(spawnPoint10);

		Spawn();
	}

	void Spawn () {

		if (spawnSpot[randomX] == spawnSpot[0]) {

			RaycastHit2D ray1 = Physics2D.Raycast(spawnPoint1, Vector2.down);

			if (ray1.collider != null && ray1.collider.tag == "Ground") {

				Instantiate(prefab, ray1.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[1]) {

			RaycastHit2D ray2 = Physics2D.Raycast(spawnPoint2, Vector2.down);

			if (ray2.collider != null && ray2.collider.tag == "Ground") {

				Instantiate(prefab, ray2.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[2]) {

			RaycastHit2D ray3 = Physics2D.Raycast(spawnPoint3, Vector2.down);

			if (ray3.collider != null && ray3.collider.tag == "Ground") {

				Instantiate(prefab, ray3.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[3]) {

			RaycastHit2D ray4 = Physics2D.Raycast(spawnPoint4, Vector2.down);

			if (ray4.collider != null && ray4.collider.tag == "Ground") {

				Instantiate(prefab, ray4.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[4]) {

			RaycastHit2D ray5 = Physics2D.Raycast(spawnPoint5, Vector2.down);

			if (ray5.collider != null && ray5.collider.tag == "Ground") {

				Instantiate(prefab, ray5.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[5]) {

			RaycastHit2D ray6 = Physics2D.Raycast(spawnPoint6, Vector2.down);

			if (ray6.collider != null && ray6.collider.tag == "Ground") {

				Instantiate(prefab, ray6.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[6]) {

			RaycastHit2D ray7 = Physics2D.Raycast(spawnPoint7, Vector2.down);

			if (ray7.collider != null && ray7.collider.tag == "Ground") {

				Instantiate(prefab, ray7.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[7]) {

			RaycastHit2D ray8 = Physics2D.Raycast(spawnPoint8, Vector2.down);

			if (ray8.collider != null && ray8.collider.tag == "Ground") {

				Instantiate(prefab, ray8.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[8]) {

			RaycastHit2D ray9 = Physics2D.Raycast(spawnPoint9, Vector2.down);

			if (ray9.collider != null && ray9.collider.tag == "Ground") {

				Instantiate(prefab, ray9.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}

		else if (spawnSpot[randomX] == spawnSpot[9]) {

			RaycastHit2D ray10 = Physics2D.Raycast(spawnPoint10, Vector2.down);

			if (ray10.collider != null && ray10.collider.tag == "Ground") {

				Instantiate(prefab, ray10.point, Quaternion.identity, parent);
				nextSpawn = false;
			}
		}
	}
}