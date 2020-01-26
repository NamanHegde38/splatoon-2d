//Script by AquaArmour

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splatoon2D {
    public class LobbyMenu : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI usernameText;

        private LoginPass _loginPass;

        private void Start() {
            SetComponents();
            SetUsername();
        }

        private void SetComponents() {
            _loginPass = FindObjectOfType<LoginPass>();
        }

        private void SetUsername() {
            usernameText.text = _loginPass.GetPlayer().DisplayName;
        }

        public void SearchGame() {
            SceneManager.LoadScene("Matchmaking");
        }
    }
}
