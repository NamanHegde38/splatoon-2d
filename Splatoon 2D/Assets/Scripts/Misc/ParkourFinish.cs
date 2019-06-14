using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkourFinish : MonoBehaviour {

	public Vector2 winSize;
	public GameObject colourBox;
	private AudioSource _audioSource;

	void Start() {

		_audioSource = GetComponent<AudioSource>();
	}

    
    void Update() {

		Collider2D[] winningPlayer = Physics2D.OverlapBoxAll(transform.position, winSize, 0);
		if (winningPlayer.Length != 0) {
			colourBox.GetComponent<Animator>().SetBool("Win", true);
			_audioSource.Play();
			this.enabled = false;
		}
	}

	void OnDrawGizmosSelected () {

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, winSize);
	}

}
