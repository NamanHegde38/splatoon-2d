//Script by AquaArmour

using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Splatoon2D {
    
    public class MatchmakingHandler : MonoBehaviourPunCallbacks {

        private MatchmakingMenu _matchmakingMenu;
        
        [SerializeField] private TextMeshProUGUI matchmakingStatus;

        private const int MaxPlayersPerRoom = 8;

        private bool _isConnecting;

        private void Awake() {
            SetSceneProperties();
        }

        private void SetSceneProperties() {
            PhotonNetwork.AutomaticallySyncScene = true;
            DontDestroyOnLoad(gameObject);
        }

        public void SearchGame() {
            _isConnecting = true;
            SetMatchmakingStatus("Searching...");

            PhotonNetwork.NickName = FindObjectOfType<LoginPass>().GetPlayer().DisplayName;
            
            if (PhotonNetwork.IsConnected) {
                PhotonNetwork.JoinRandomRoom();
            }
            else {
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster() {
            Debug.Log("Connected to master");
            
            if (_isConnecting) {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            SetMatchmakingStatus("Creating match");
            PhotonNetwork.CreateRoom(null, new RoomOptions {
                MaxPlayers = MaxPlayersPerRoom
            });
        }

        public override void OnJoinedRoom() {
            Debug.Log("Client successfully joined a room");
            SetMatchmakingStatus("Match found");
            SceneManager.LoadScene("Matchmaking");
            gameObject.AddComponent<MatchmakingMenu>();
            _matchmakingMenu = GetComponent<MatchmakingMenu>();
            _matchmakingMenu.ResetPanels();

            for (var i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++) {
                _matchmakingMenu.OccupyPanel(i, PhotonNetwork.PlayerList[i].NickName);
            }
        }

        public override void OnDisconnected(DisconnectCause cause) {
            SceneManager.LoadScene("Lobby");
            Destroy(GetComponent<MatchmakingMenu>());
            if (FindObjectsOfType<MatchmakingHandler>().Length > 1) {
                Destroy(this);
            }
        }
        
        private void SetMatchmakingStatus(string status) {
            matchmakingStatus.text = status;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer) {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Matchmaking")) return;
            _matchmakingMenu.OccupyPanel(PhotonNetwork.CurrentRoom.PlayerCount - 1, newPlayer.NickName);
        }
    }
}
