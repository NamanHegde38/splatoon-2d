using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkourFinish : MonoBehaviour {

	public Vector2 winSize;
	public GameObject colourBox;
	public GameObject audioSource;
    
    void Start() {
        
		
    }

    
    void Update() {

		Collider2D[] winningPlayer = Physics2D.OverlapBoxAll(transform.position, winSize, 0);
		if (winningPlayer.Length != 0) {
			colourBox.GetComponent<Animator>().SetBool("Win", true);
			audioSource.GetComponent<AudioSource>().Play();
			this.enabled = false;
		}
	}

	void OnDrawGizmosSelected () {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, winSize);
	}

}
