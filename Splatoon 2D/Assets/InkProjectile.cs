using System;
using Player;
using UnityEngine;
using Weapon.Shooters;
using Random = UnityEngine.Random;

public class InkProjectile : MonoBehaviour {

    private ShooterManager _shooterManager;
    private PlayerManager _playerManager;

    private Rigidbody2D _rigidBody;
        
    private void Start() {
        SetComponents();
        SetBloom();
    }

    private void SetComponents() {
        _shooterManager = FindObjectOfType<ShooterManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void SetBloom() {
        var angle = _shooterManager.shooter.bloom / 2f;
        var randomDirection = Random.Range(-angle, angle);
        SetRange(randomDirection);
    }
    
    private void SetRange(float direction) {
        var range = _shooterManager.shooter.range;
        ShootProjectile(range, direction);
    }
    
    private void ShootProjectile(float force, float direction) {
        _rigidBody.AddRelativeForce(new Vector2(force, direction), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag(_playerManager.unpaintedGround) || col.CompareTag(_playerManager.enemyPaintedGround)) {
            col.gameObject.tag = _playerManager.teamPaintedGround;
        }

        if (!col.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
