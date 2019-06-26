using System.Globalization;
using TMPro;
using UnityEngine;

//Script by AquaArmour

namespace Misc
{
	public class FrameCount : MonoBehaviour {

		public float refreshTime = 0.5f;

		private int _frameCounter;
		private float _timeCounter;
		private float _lastFramerate;
		private TextMeshProUGUI _text;

		private void Start() {

			_text = GetComponent<TextMeshProUGUI>();
		}

		private void Update() {

			if (_timeCounter < refreshTime) {
				_timeCounter += Time.deltaTime;
				_frameCounter++;
			}

			else {
				_lastFramerate = _frameCounter / _timeCounter;
				_frameCounter = 0;
				_timeCounter = 0.0f;
			}

			_text.text = Mathf.Round(_lastFramerate).ToString(CultureInfo.InvariantCulture) + " fps";
		}
	}
}
