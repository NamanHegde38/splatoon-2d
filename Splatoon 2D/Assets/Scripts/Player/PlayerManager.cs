using UnityEngine;

namespace Player {
	
	[RequireComponent(typeof(PlayerController))]
	public class PlayerManager : MonoBehaviour {

		private PlayerController _controller;
		private PlayerHealth _playerHealth;
		
		private LayerMask _collisionMask;
		
		[HideInInspector] public string groundInk;
		public string unpaintedGround;
		public string teamPaintedGround;
		public string enemyPaintedGround;

		private void Start() {
			SetComponents();
		}

		private void SetComponents() {
			_controller = GetComponent<PlayerController>();
			_playerHealth = GetComponent<PlayerHealth>();
			_collisionMask = _controller.collisionMask;
		}

		private void Update() {
			CheckGroundInk();
			OnEnemyInk();
		}

		private string CheckGroundTag () {

			var hit = Physics2D.Raycast(transform.position, Vector2.down, .5f, _collisionMask);

			if (hit) {
				var groundTag = hit.collider.tag;
				return groundTag;
			}

			else {
				return null;
			}
		
		}
		
		private void CheckGroundInk() {
			if (_controller.collisions.Below) {
				groundInk = CheckGroundTag();
			}
		}

		private void OnEnemyInk() {
			if (groundInk == enemyPaintedGround) {
				if (_playerHealth.health > 25) {
					_playerHealth.TakeDamage(1);
				}
			}
		}
		
		

	}
}
