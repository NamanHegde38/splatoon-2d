//Script by AquaArmour

using System;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Splatoon2D {
    public class GameHandler : MonoBehaviour {
        
        [SerializeField]
        [AssetSelector]
        [AssetsOnly]
        private GameObject playerPrefab;

        private void Start() {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 0, -15), Quaternion.identity);
        }
    }
}