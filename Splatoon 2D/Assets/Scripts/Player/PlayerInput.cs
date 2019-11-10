using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerHandler))]
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour {

        private PlayerHandler _playerHandler;
        private PlayerMovement _player;

        private void Start() {
            _player = GetComponent<PlayerMovement>();
            _playerHandler = GetComponent<PlayerHandler>();
        }

        private void Update() {
            var directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _player.SetDirectionalInput(directionalInput);

            if (Input.GetKeyDown(KeyCode.Space)) {
                _player.OnJumpInputDown();
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                _player.OnJumpInputUp();
            }
        }
    }
}