//Script by AquaArmour

using Sirenix.OdinInspector;
using Photon.Pun;
using UnityEngine;

namespace Splatoon2D {
    public class PlayerMovement : MonoBehaviourPun {

        [SerializeField]
        [ReadOnly]
        [BoxGroup("Input")]
        private Vector2 directionalInput;
        
        [SerializeField]
        [PropertyRange(1, 10)]
        private float moveSpeed;

        private bool isMine;
        private Rigidbody2D _rigidBody;

        private void Awake() {
            SetComponents();
        }

        private void SetComponents() {
            _rigidBody = GetComponent<Rigidbody2D>();
            isMine = photonView.IsMine;
        }

        private void Update() {
            if (isMine) {
                GetDirectionalInput();
            }
        }

        private void GetDirectionalInput() {
            directionalInput = new Vector2 {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };
            SetDirectionalInput(directionalInput);
        }
        
        private void SetDirectionalInput(Vector2 input) {
            directionalInput = input;
            directionalInput.Normalize();
        }

        private void FixedUpdate() {
            if (isMine) {
                MovePlayer();
            }
        }

        private void MovePlayer() {
            _rigidBody.velocity = directionalInput * moveSpeed;
        }
    }
}