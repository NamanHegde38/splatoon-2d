using UnityEngine;
using Weapon.Shooter;

namespace Weapon {
    public class WeaponInput : MonoBehaviour {

        private ShooterHandler _shooterHandler;

        private bool _shooter;
        private float _fireRate;
        private float _timeBtwShots;

        private void Awake() {
            _shooterHandler = GetComponent<ShooterHandler>();
        }

        private void Start() {
            if (_shooterHandler != null) {
                _shooter = true;
                _fireRate = _shooterHandler.shooter.fireRate / 60;
            }

            else {
                _shooter = false;
            }
        }

        private void Update() {
            if (_timeBtwShots <= 0) {
                if (_shooter) {
                    if (Input.GetButton("Fire1")) {
                        _shooterHandler.Shoot();
                        _timeBtwShots = _fireRate;
                    }
                }
            }

            else {
                _timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
