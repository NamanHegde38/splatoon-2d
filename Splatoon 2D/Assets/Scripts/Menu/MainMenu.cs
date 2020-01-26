//Script by AquaArmour

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splatoon2D {
    public class MainMenu : MonoBehaviour {

        // ReSharper disable once MemberCanBePrivate.Global
        public void LoadGame() {
            SceneManager.LoadScene("Login");
        }
    }
}