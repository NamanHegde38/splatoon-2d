using System;
using UnityEngine;

namespace Player {
    public class BetterJump : MonoBehaviour {

        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;

        private PlayerMovement _playerMovement;

        private void Awake() {
            SetComponents();
        }

        private void SetComponents() {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void FixedUpdate() {
            SetGravityScale();
        }

        private void SetGravityScale() {
            if (_playerMovement.velocity.y < 0) {
                _playerMovement.gravityScale = fallMultiplier;
            }
            else if (_playerMovement.velocity.y > 0 && !Input.GetButton("Jump")) {
                _playerMovement.gravityScale = lowJumpMultiplier;
            }
            else {
                _playerMovement.gravityScale = 1f;
            }
        }
    }
}
