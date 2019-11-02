using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour {

        private PlayerMovement _player;

        private void Start() {
            _player = GetComponent<PlayerMovement>();
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