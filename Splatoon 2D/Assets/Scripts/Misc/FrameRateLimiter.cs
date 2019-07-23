using UnityEngine;

namespace Misc {
    public class FrameRateLimiter : MonoBehaviour
    {
        // Start is called before the first frame update

        // Update is called once per frame
        private void Update() {
            Application.targetFrameRate = 60;
        }
    }
}
