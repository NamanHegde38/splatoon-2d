using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;

	float respawnCd;

    void Start () {
        

    }

    void Update () {
      
		
    }

	public void Die (GameObject player, bool respawn, float respawnTime) {

		Destroy(player);

		if (respawn) {

			if (respawnCd <= 0) {
				Instantiate(playerPrefab, new Vector2(26f, 6.5f), Quaternion.identity);
				respawnCd = respawnTime;
			}

			else {
				respawnCd -= Time.deltaTime;
			}
		}
	}

	public void InstantRespawn (GameObject player, Vector2 respawnPos) {

		player.transform.position = respawnPos;
	}
}
