using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrameLimit : MonoBehaviour {

	//Script by AquaArmour

	void Awake () {

		Application.targetFrameRate = 60;
	}
}
