using UnityEngine;
using TMPro;

namespace Misc {
    public class FrameRateHandler : MonoBehaviour {

        [SerializeField] private int frameRateLimit;
        [SerializeField] private float updateInterval;
        [SerializeField] private TextMeshProUGUI frameText;
        
        private int _frameRate;
        private float _updateTimer;
        
        private void Awake() {
            Application.targetFrameRate = frameRateLimit;
        }

        private void Update() {
            if (_updateTimer <= 0) {
                _frameRate = (int)(1f / Time.unscaledDeltaTime);
                frameText.text = _frameRate + " Fps";
                _updateTimer = updateInterval;
            }
            else {
                _updateTimer -= Time.deltaTime;
            }
            
            
        }
    }
}
