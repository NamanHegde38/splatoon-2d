//Script by AquaArmour

using Sirenix.OdinInspector;
using UnityEngine;

namespace Splatoon2D {
    public class FrameRateHandler : MonoBehaviour {

        [SerializeField]
        [PropertyRange(30, 999)]
        [OnValueChanged(nameof(SetFrameRate))]
        [BoxGroup("Settings")]
        private int targetFrameRate;

        private void Awake() {
            SetFrameRate();
        }

        private void SetFrameRate() {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}