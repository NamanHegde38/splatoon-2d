using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	[SerializeField] Vector2 shredSize = new Vector2(10, 10);
	[SerializeField] Vector2 respawnPosition = new Vector2(0, 0);

	GameManager gameManager;
    
    void Start() {

		gameManager = FindObjectOfType<GameManager>();
    }

    
    void Update() {

		Collider2D[] playersToKill = Physics2D.OverlapBoxAll(transform.position, shredSize, 0);
		for (int i = 0; i < playersToKill.Length; i++) {
			gameManager.InstantRespawn(playersToKill[i].gameObject, respawnPosition);
		}
    }

	void OnDrawGizmosSelected () {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, shredSize);
	}
}
