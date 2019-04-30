using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collision) {
		collision.gameObject.transform.position = new Vector2 (26, 10);
	}
}
