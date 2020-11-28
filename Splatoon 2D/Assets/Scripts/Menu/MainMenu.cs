//Script by AquaArmour
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splatoon2D {
    public class MainMenu : MonoBehaviour {

        public void LoadGame() {
            SceneManager.LoadScene("Login");
        }
    }
}