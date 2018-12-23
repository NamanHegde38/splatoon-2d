using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	private VideoPlayer videoPlayer;

	void Start() {

		videoPlayer = GetComponent<VideoPlayer>();
	}

	void Update() {

		if (!videoPlayer.isPlaying) {
			SceneManager.LoadScene("Test");
		}

	}

}
