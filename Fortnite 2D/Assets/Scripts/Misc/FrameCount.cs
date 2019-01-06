using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

//Script by AquaArmour

public class FrameCount : MonoBehaviour {

	public float m_refreshTime = 0.5f;

	private int m_frameCounter = 0;
	private float m_timeCounter = 0.0f;
	private float m_lastFramerate = 0.0f;
	private TextMeshProUGUI text;

	void Start() {

		text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {

		if (m_timeCounter < m_refreshTime) {
			m_timeCounter += Time.deltaTime;
			m_frameCounter++;
		}

		else {
			m_lastFramerate = (float)m_frameCounter / m_timeCounter;
			m_frameCounter = 0;
			m_timeCounter = 0.0f;
		}

		text.text = Mathf.Round(m_lastFramerate).ToString() + " fps";
	}
}
