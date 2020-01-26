//Script by AquaArmour

using PlayFab.ClientModels;
using UnityEngine;

namespace Splatoon2D {
    public class LoginPass : MonoBehaviour {

        private PlayerProfileModel _player;

        private void Awake() {
            var loginPassCount = FindObjectsOfType<LoginPass>().Length;
            if (loginPassCount > 1) {
                Destroy(gameObject);
            }
            else {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void SetPlayer(PlayerProfileModel player) {
            _player = player;
        }

        public PlayerProfileModel GetPlayer() {
            return _player;
        }
    }
}
