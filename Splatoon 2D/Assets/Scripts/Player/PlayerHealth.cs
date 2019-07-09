using System;
using UnityEngine;

namespace Player {
	[RequireComponent(typeof(PlayerSquid))]
	public class PlayerHealth : MonoBehaviour {

		private PlayerSquid _squid;

		public int health = 100;
		
		private bool _hasTakenDamage;
		private float _timeTillDamageFades;

		private float _timeBetweenRegeneration;
		
		private void Start() {
			SetComponents();
		}

		private void SetComponents() {
			_squid = GetComponent<PlayerSquid>();
		}

		private void Update() {
			CheckIfDead();
			SetDamageFadeTimer();
			RegenerateHealth();
		}

		public void TakeDamage(int damage) {
			health -= damage;
			_hasTakenDamage = true;
		}

		private void SetDamageFadeTimer() {
			if (_hasTakenDamage) {
				if (_timeTillDamageFades <= 0) {
					_timeTillDamageFades = _squid.isSquid ? 5f : 7.5f;
					_hasTakenDamage = false;
				}

				else {
					_timeTillDamageFades -= Time.deltaTime;
				}
			}
		}

		private void RegenerateHealth() {
			if (!_hasTakenDamage) {
				if (100 > health) {
					if (_timeBetweenRegeneration <= 0) {
						_timeBetweenRegeneration = _squid.isSquid ? 0.5f : 1f;
						health += 10;
					}

					else {
						_timeBetweenRegeneration -= Time.deltaTime;
					}
				}
			}
		}
		
		private void CheckIfDead() {
			if (health <= 0) {
				Die();
			}
		}

		private static void Die() {
			
		}
	}
}
