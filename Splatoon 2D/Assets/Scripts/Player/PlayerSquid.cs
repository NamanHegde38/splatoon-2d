using UnityEngine;

namespace Player {
	public class PlayerSquid : MonoBehaviour {
		
		public bool isSquid;

		public Material sprite;

		[HideInInspector] public bool receivingCrouchInput;

		[SerializeField] private Vector2 inklingSize = new Vector2(1f, 1f);
		[SerializeField] private Vector2 squidSize = new Vector2(1f, 0.25f);
		private BoxCollider2D _boxCollider2D;

		private void Start() {
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}

		private void Update() {
			// ReSharper disable once Unity.PerformanceCriticalCodeInvocation
			CheckIfSquid();
		}

		public void SetIsSquid(bool crouchInput) {
			receivingCrouchInput = crouchInput;
		}

		private void SquidForm() {
			if (!isSquid) {
				_boxCollider2D.size = squidSize;
				_boxCollider2D.offset = new Vector2(0, -0.37f);
				sprite.color = Color.blue;
				isSquid = true;
			}
		}

		private void InklingForm() {
			if (isSquid) {
				_boxCollider2D.size = inklingSize;
				_boxCollider2D.offset = new Vector2(0, 0);
				sprite.color = Color.red;
				isSquid = false;
			}
		}

		private void CheckIfSquid() {
			if (receivingCrouchInput) {
				SquidForm();
			}
			else {
				InklingForm();
			}
		}
	}
}