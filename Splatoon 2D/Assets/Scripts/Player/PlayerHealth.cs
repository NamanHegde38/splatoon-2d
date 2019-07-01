using System;
using UnityEngine;

namespace Player {
	public class PlayerHealth : MonoBehaviour {

		public int health = 100;

		public void TakeDamage(int damage) {
			health -= damage;
		}

		private void CheckHealth() {
			if (health <= 0) {
				Die();
			}
		}

		private static void Die() {
			
		}
	}
}
