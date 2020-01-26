//Script by AquaArmour

using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Splatoon2D {
    
    public class MatchmakingMenu : MonoBehaviour {

        private readonly GameObject[] _searchingPanels = new GameObject[7];
        private GameObject _searchPanelMaster;

        private void Awake() {
            SetupPanels();
        }

        private void SetupPanels() {
            _searchPanelMaster = GameObject.Find("Searching Fields");
            for (var i = 0; i < _searchingPanels.Length; i++) {
                _searchingPanels[i] = _searchPanelMaster.transform.GetChild(i).gameObject;
            }
            ResetPanels();
        }

        public void OccupyPanel(int panel, string playerName) {
            _searchingPanels[panel].GetComponent<Image>().color = Color.white;
            _searchingPanels[panel].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerName;
            _searchingPanels[panel].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
        }
        
        public void ResetPanel(int panel) {
            _searchingPanels[panel].GetComponent<Image>().color = Color.black;
            _searchingPanels[panel].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Searching...";
            _searchingPanels[panel].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        }
        
        public void ResetPanels() {
            for (var i = 0; i < 8; i++) {
                _searchingPanels[i].GetComponent<Image>().color = Color.black;
                _searchingPanels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Searching...";
                _searchingPanels[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
        }

        public void StartGame() {
            PhotonNetwork.LoadLevel("Game");   
        }
    }
}
