using UnityEngine;

namespace Player.PlayerPhysics
{
	public class PlayerSquid : MonoBehaviour {
    
		private PlayerController _controller;

		private bool _isSquid;

		public Material sprite;

		[HideInInspector] public bool receivingCrouchInput;

		[SerializeField] private Vector2 inklingSize = new Vector2(1f, 1f);
		[SerializeField] private Vector2 squidSize = new Vector2(1f, 0.25f);
    
		private void Start()
		{
			CheckIfSquid();
		}

		private void SetComponents() {
			_controller = GetComponent<PlayerController>(); 
		}

		private void Update() {
		}

		public void SetIsSquid(bool crouchInput) {
			if (crouchInput) {
				receivingCrouchInput = true;
			}
			else {
				receivingCrouchInput = false;
			}
		}

		private void SquidForm() {
			if (!_isSquid)
			{
				
			} 
			
			GetComponent<BoxCollider2D>().size = squidSize;
			GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.37f);
			sprite.color = Color.blue;
		}

		private void InklingForm() {
			GetComponent<BoxCollider2D>().size = inklingSize;
			GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
			sprite.color = Color.red;
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
